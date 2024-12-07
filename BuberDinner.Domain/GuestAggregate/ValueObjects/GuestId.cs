using BuberDinner.Domain.Common.Models;
namespace BuberDinner.Domain.GuestAggregate.ValueObjects;

public sealed class GuestId : ValueObject
{
    public Guid Value { get; }
    private GuestId(Guid value)
    {
        Value = value;
    }
    public static GuestId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public static GuestId Create(string id)
    {
        return new(Guid.Parse(id));
    }
    public static GuestId Create(Guid id)
    {
        return new(id);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
