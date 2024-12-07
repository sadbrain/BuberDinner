using BuberDinner.Domain.Common.Models;
namespace BuberDinner.Domain.HostAggregate.ValueObjects;

public sealed class HostId : ValueObject
{
    public Guid Value { get; }
    private HostId(Guid value)
    {
        Value = value;
    }
    public static HostId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public static HostId Create(string id)
    {
        return new(Guid.Parse(id));
    }
    public static HostId Create(Guid id)
    {
        return new(id);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
