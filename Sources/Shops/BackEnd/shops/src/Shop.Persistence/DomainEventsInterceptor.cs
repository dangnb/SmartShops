using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shop.Domain.Abstractions;

namespace Shop.Persistence;

public sealed class DomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IMediator _mediator;
    public DomainEventsInterceptor(IMediator mediator) => _mediator = mediator;

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var ctx = eventData.Context;
        if (ctx is null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        // Dispatch all domain events (loop để bắt event phát sinh thêm)
        while (true)
        {
            var entities = ctx.ChangeTracker.Entries<Entity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Count > 0)
                .ToList();

            var events = entities.SelectMany(e => e.DomainEvents).ToList();
            if (events.Count == 0)
                break;

            entities.ForEach(e => e.ClearDomainEvents());

            foreach (var ev in events)
                await _mediator.Publish(ev, cancellationToken);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
