using BuberDinner.Domain.MenuAggregate.Events;
using MediatR;

namespace BuberDinner.Application.Menus.Commands.Events;

public class MenuCreatedEventHandler : INotificationHandler<MenuCreated>
{
    public Task Handle(
        MenuCreated notification, 
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
