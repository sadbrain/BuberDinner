namespace BuberDinner.Domain.Common.Models;
//aggregateRoot cung lla mot entity => wrapper xung quanh cai entity
public abstract class AggregateRoot<TId, TIdType> : Entity<TId>
    where TId : AggregateRootId<TIdType>
{
    public new TId Id { get; private set; }
    protected AggregateRoot(TId id) : base(id)
    {
        Id = id;
    }

#pragma warning disable CS8618
    protected AggregateRoot()
    {

    }
#pragma warning disable CS8618
}
