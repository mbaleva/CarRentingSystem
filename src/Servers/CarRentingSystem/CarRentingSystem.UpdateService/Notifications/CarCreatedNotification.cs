using CarRentingSystem.Common.Messages;
using CarRentingSystem.UpdateService.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace CarRentingSystem.UpdateService.Notifications;

public class CarCreatedNotification : IConsumer<CarCreatedMessage>
{

    private readonly IHubContext<NotificationsHub> hub;

    public CarCreatedNotification(IHubContext<NotificationsHub> hub)
        => this.hub = hub;

    public async Task Consume(ConsumeContext<CarCreatedMessage> context)
    {
        await this.hub.Clients
            .Groups(GlobalConstants.AuthenticatedUsersGroup)
            .SendAsync(GlobalConstants.ReceiveNotificationEndpoint, context.Message);
    }
}
