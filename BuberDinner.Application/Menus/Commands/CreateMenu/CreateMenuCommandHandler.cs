using MediatR;
using ErrorOr;

using BuberDinner.Domain.MenuAggregate;
using BuberDinner.Domain.MenuAggregate.Entities;
using BuberDinner.Domain.HostAggregate.ValueObjects;
using BuberDinner.Application.Common.Interfaces.Persistence;

namespace BuberDinner.Application.Menus.Commands.CreateMenu;

public class CreateMenuCommandHandler :
    IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
    private readonly IMenuRepository _menuRepository;

    public CreateMenuCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }
    public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        //create menu
        var menu = Menu.Create(
            HostId.Create(command.HostId),
            command.Name,
            command.Description,
            command.Sections.ConvertAll(s => MenuSection.Create(
                s.Name,
                s.Description,
                s.Items.ConvertAll(i => MenuItem.Create(
                    i.Name,
                    i.Description)))));
        //Persist menu
        _menuRepository.Add(menu);
        //return menu
        return menu;
    }
}

