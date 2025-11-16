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
        if (!IsCommand())
            return await next();

        var strategy = _context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            var response = await next();

            ApplyAuditInfo(); // <-- audit ở đây

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return response;
        });
    }

    private bool IsCommand()
        => typeof(TRequest).Name.EndsWith("Command");

    private void ApplyAuditInfo()
    {


        foreach (var entry in _context.ChangeTracker.Entries())
        {
            if (entry.Entity is not IAuditable auditEntity)
                continue;

            var userId = _currentUser.UserId ?? "system";
            var now = DateTime.UtcNow;
            var comId = _currentUser.GetRequiredCompanyId();

            // Set ComId nếu entity có scope theo company
            if (entry.Entity is ICompanyScopedEntity companyScoped &&
                entry.State == EntityState.Added &&
                companyScoped.ComId == Guid.Empty)
            {
                companyScoped.ComId = comId;
            }

            switch (entry.State)
            {
                case EntityState.Added:
                    auditEntity.CreatedAt = now;
                    auditEntity.CreatedBy = userId;
                    auditEntity.IsDeleted = false;
                    auditEntity.DeletedAt = null;
                    break;

                case EntityState.Modified:
                    auditEntity.LastModifiedAt = now;
                    auditEntity.LastModifiedBy = userId;
                    break;

                case EntityState.Deleted:
                    // Soft delete
                    entry.State = EntityState.Modified;
                    auditEntity.IsDeleted = true;
                    auditEntity.DeletedAt = now;
                    auditEntity.LastModifiedAt = now;
                    auditEntity.LastModifiedBy = userId;
                    break;
            }
        }
    }
}
