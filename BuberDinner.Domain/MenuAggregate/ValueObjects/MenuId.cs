using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.HostAggregate.ValueObjects;
namespace BuberDinner.Domain.MenuAggregate.ValueObjects;

public sealed class MenuId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }
    private MenuId(Guid value)
    {
        Value = value; 
    }
    public static MenuId Create(string id)
    {
        return new(Guid.Parse(id));
    }
    public static MenuId Create(Guid id)
    {
        return new(id);
    }
    public static MenuId CreateUnique()
    {
        return new (Guid.NewGuid()); 
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
