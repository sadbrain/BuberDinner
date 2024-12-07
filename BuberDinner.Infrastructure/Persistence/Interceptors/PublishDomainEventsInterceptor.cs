using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using BuberDinner.Domain.Common.Models;
using MediatR;
namespace BuberDinner.Infrastructure.Persistence.Interceptors;
//SaveChangesInterceptor chua nhung method duoc goi truoc khi save change
//goi sau khi savechage

public class PublishDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _mediator;
    public PublishDomainEventsInterceptor(IPublisher mediator)
    {
        _mediator = mediator;
    }
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        await PublishDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEvents(DbContext? dbContext)
    {
        if (dbContext is null) return;
        //get hold of all the various entities
        //DomainEvents of IHasDomainEvents
        //Entity Micro.EntityEntry <IHasDomainEvents>
        var entitiesWithDomainEvents = dbContext.ChangeTracker.Entries<IHasDomainEvents>()
            .Where(entry => entry.Entity.DomainEvents.Any())
            .Select(entry => entry.Entity)
            .ToList();
        //get hold of all the various domain events
        var domainEvents = entitiesWithDomainEvents.SelectMany(
            entry => entry.DomainEvents).ToList();
        //clear domain
        entitiesWithDomainEvents.ForEach(entity => entity.ClearDomainEvents());
        //publish domain events
        foreach (var domainEvent in domainEvents)
        { 
            await _mediator.Publish(domainEvent);
        }

    }
}
