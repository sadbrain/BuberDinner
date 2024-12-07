using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.MenuAggregate.ValueObjects;
using BuberDinner.Domain.MenuAggregate.Entities;
using BuberDinner.Domain.MenuAggregate.Events;
using BuberDinner.Domain.MenuReviewAggregate.ValueObjects;
using BuberDinner.Domain.HostAggregate.ValueObjects;
using BuberDinner.Domain.DinnerAggregate.ValueObjects;

namespace BuberDinner.Domain.MenuAggregate;
//chúng ta sẽ tạo valueObject that will container Id itself
//we can update id => change it from a Guild to a string or the format 
public sealed class Menu : AggregateRoot<MenuId, Guid>
{

    private readonly List<MenuSection> _sections = new(); 
    private readonly List<DinnerId> _dinnerIds = new(); 
    private readonly List<MenuReviewId> _menuReviewIds = new();

    public string Name { get; private set; }
    public string Description { get; private set; }
    public AverageRating AverageRating { get; private set;  }
    public HostId HostId { get; private set; }
    public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();

    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
    public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public Menu(
        MenuId id,
        HostId hostId,
        string name,
        string description,
        AverageRating averageRating,
        List<MenuSection>? sections,
        DateTime createdDateTime,
        DateTime updatedDateTime
        )
: base(id)
    {
        Name = name;
        Description = description;
        HostId = hostId;
        _sections = sections;
        AverageRating = averageRating;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;  
    }
    public static Menu Create(
        HostId hostId,
        string name, 
        string description,
        List<MenuSection>? sections)
    {
        var menu = new Menu(
            MenuId.CreateUnique(),
            hostId,
            name,
            description,
            AverageRating.CreateNew(),
            sections ?? new(),
            DateTime.UtcNow,
            DateTime.UtcNow);

        menu.AddDomainEvent(new MenuCreated(menu));

        return menu;
    }
#pragma warning disable CS8618
    private Menu()
    {
        
    }
#pragma warning disable CS8618

}
