using BuberDinner.Domain.Common.Models;
namespace BuberDinner.Domain.Common.ValueObjects;

public class Rating :  ValueObject
{
    public double Value { get; private set; }
    private Rating(double value)
    {
        Value = value;
    }
    public static Rating CreateNew(double rating = 0)
    {
        return new(rating);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
