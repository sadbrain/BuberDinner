using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.UserAggregate.ValueObjects;
using BuberDinner.Domain.HostAggregate.ValueObjects;
using BuberDinner.Domain.GuestAggregate.ValueObjects;

namespace BuberDinner.Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId, Guid>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public HostId HostId { get; private set; }
    public GuestId GuestId { get; private set; }

    public User(
        UserId id, 
        string email, 
        string password, 
        string lastName, 
        string firstName)
        : base(id)
    {
        Email = email;
        Password = password;
        LastName = lastName;
        FirstName = firstName;
        HostId = HostId.CreateUnique();
        GuestId = GuestId.CreateUnique();
    }
    public static User Create(
        string email,
        string password,
        string lastName,
        string firstName)
    {
        return new(
            UserId.CreateUnique(),
            email,
            password,
            lastName,
            firstName
            );
    }
#pragma warning disable CS8618
    private User()
    {

    }
#pragma warning disable CS8618
}
