
namespace BuberDinner.Domain.Common.Models;

public abstract class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvents
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public TId Id { get; protected set; } = default!;
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    protected Entity(TId id) 
    {
        Id = id;
    }
    //two entity is equal
    public override bool Equals(object? obj)
    {
        //entity duoc tao tu lenh  obj is Entity<TId>   
        //entity la mot doi tuong co mot thuoc tinh nhan dien duy nhat ID
        //=> xem la bang nhau khi co ID bang nhau
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?) other);
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }
    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    public void ClearDomainEvents()
    {
        _domainEvents.Clear(); 
    }


#pragma warning disable CS8618
    protected Entity()
    {

    }
#pragma warning disable CS8618
}
