namespace BuberDinner.Domain.Common.Models;

public abstract class ValueObject : IEquatable<ValueObject>
{
    //IEquatable cung cấp các loại tuỳ chỉnh để kiểm tra sự bằng nhau của hai object
    //ValueObject là lớp có sỡ trừu tượng cho các đối tượng giá trị => các object không có danh tính
    //và so sanh dựa trên giá trị thay cho danh tính
    public abstract IEnumerable<object> GetEqualityComponents();
    //phương thức của IEquatable<ValueObject>  không cần kiểm kiểu vì tham số đã đủ xác định rồi
    public bool Equals(ValueObject? other)
    {
        return Equals((object?)other);
    }
    //make sure return the same value compare the properties
    public override bool Equals(object? obj)
    {
        //make sure 2 object is the same type
        if(obj is null || obj.GetType() != GetType())
        {
            return false;
        }
        var valueObject = (ValueObject)obj;
        //the valueObject sẽ có method GetEqualityComponents
        //this method return the properites in a specific order then 
        //return như này tưc là sẽ lấy những properites của obj 1 và gọi hàm SequenceEqual để so sánh lần lượt
        //giá trị cũng sẽ được truyên lần lượt từ GetEqualityComponents của object hai => value object
        return GetEqualityComponents()
            .SequenceEqual(valueObject.GetEqualityComponents());
    }

    public static bool operator ==(ValueObject left, ValueObject right) 
    { 
        //phương thức dưới của object nó sẽ gọi Equals(object? obj) mà mình đã ghi đè
        return Equals(left, right);
    }
    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !Equals(left, right);
    }

    //lấy mã băm của một đối tượng (lưu trữ bằng dictionary và tra cứu nhanh chóng)
    
    public override int GetHashCode()
    {
        //Aggregate giúp gộp một dãy các phần tử thành một giá trị nhanh nhất
        //x^y XOR trên các giá trị băm => kết hợp tất cả các mã băm thành một giá trị băm duy nhất cho toàn bộ valueobject
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }


}
public class Price : ValueObject
{
    public decimal Amount { get; private set; } 
    public string Currency {  get; private set; }
    public Price(decimal amount, string currency)
    {
        Amount = amount;
       Currency = currency;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        //trả lần lượt các thuộc tính 
        yield return Amount;
        yield return Currency;
    }
}
