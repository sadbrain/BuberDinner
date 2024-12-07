using BuberDinner.Domain.Common.Models;
namespace BuberDinner.Domain.MenuAggregate.ValueObjects;

public sealed class MenuSectionId : ValueObject
{
    public Guid Value { get; }
    private MenuSectionId(Guid value)
    {
        Value = value;
    }
    public static MenuSectionId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public static MenuSectionId Create(Guid id)
    {
        return new(id);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
