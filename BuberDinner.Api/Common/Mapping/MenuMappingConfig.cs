using Mapster;

using BuberDinner.Contracts.Menus;
using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Domain.MenuAggregate;
using MenuSection = BuberDinner.Domain.MenuAggregate.Entities.MenuSection;
using MenuItem = BuberDinner.Domain.MenuAggregate.Entities.MenuItem;
namespace BuberDinner.Api.Common.Mapping;

public class MenuMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateMenuRequest Request, string HostId), CreateMenuCommand>()
            .Map(d => d.HostId, src => src.HostId)
            .Map(d => d, src => src.Request);

        config.NewConfig<Menu, MenuResponse>()
            .Map(d => d.Id, src => src.Id.Value)
            .Map(d => d.HostId, src => src.HostId.Value)
            .Map(d => d.DinnerIds, src => src.DinnerIds.Select(d => d.Value))
            .Map(d => d.MenuReviewIds, src => src.MenuReviewIds.Select(m => m.Value));
            

        config.NewConfig<MenuSection, MenuSectionResponse>()
            .Map(d => d.Id, src => src.Id.Value);

        config.NewConfig<MenuItem, MenuItemResponse>()
            .Map(d => d.Id, src => src.Id.Value);
    }
}
