using Microsoft.AspNetCore.SignalR;

namespace CarRentingSystem.UpdateService.Hubs;

public class NotificationsHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        if (this.Context.User.Identity.IsAuthenticated)
        {
            await this.Groups.AddToGroupAsync(
                this.Context.ConnectionId,
                GlobalConstants.AuthenticatedUsersGroup);
        }
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        if (this.Context.User.Identity.IsAuthenticated)
        {
            await this.Groups.RemoveFromGroupAsync(
                this.Context.ConnectionId,
                GlobalConstants.AuthenticatedUsersGroup);
        }
    }
}
