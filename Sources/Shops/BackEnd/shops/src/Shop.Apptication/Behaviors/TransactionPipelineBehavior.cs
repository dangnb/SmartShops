using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Contract;
using Shop.Domain.Abstractions.Entities;
using Shop.Persistence;

namespace Shop.Application.Behaviors;

public sealed class TransactionPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;

    public TransactionPipelineBehavior(
        ApplicationDbContext context,
        ICurrentUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Query thì không mở transaction
        if (!IsCommand())
            return await next();

        var strategy = _context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                // 1️⃣ Handler chỉ modify entity, KHÔNG SaveChanges
                var response = await next();

                // 2️⃣ Apply audit CHỈ cho entity thực sự thay đổi
                ApplyAuditInfo();

                // 3️⃣ SaveChanges tập trung 1 chỗ
                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return response;
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw; // hoặc map sang BusinessException nếu bạn muốn
            }
        });
    }

    private static bool IsCommand()
        => typeof(TRequest).Name.EndsWith("Command");

    private void ApplyAuditInfo()
    {
        var now = DateTime.UtcNow;
        var userId = _currentUser.UserId ?? "system";
        var comId = _currentUser.ComId;

        // ⚠️ CHỈ xử lý entity có thay đổi state
        var entries = _context.ChangeTracker
            .Entries<IAuditable>()
            .Where(e =>
                e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted);

        foreach (var entry in entries)
        {
            // Set ComId khi tạo mới
            if (entry.Entity is ICompanyScopedEntity companyScoped &&
                entry.State == EntityState.Added &&
                comId.HasValue &&
                companyScoped.ComId == Guid.Empty)
            {
                companyScoped.ComId = comId.Value;
            }

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = now;
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.IsDeleted = false;
                    entry.Entity.DeletedAt = null;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedAt = now;
                    entry.Entity.LastModifiedBy = userId;
                    break;

                case EntityState.Deleted:
                    // Soft delete
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedAt = now;
                    entry.Entity.LastModifiedAt = now;
                    entry.Entity.LastModifiedBy = userId;
                    break;
            }
        }
    }
}
