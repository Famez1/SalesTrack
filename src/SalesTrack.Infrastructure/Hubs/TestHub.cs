using Microsoft.AspNetCore.SignalR;
using SalesTrack.Abstractions.Infrastructure;

namespace SalesTrack.Infrastructure.Hubs;

public class TestHub : Hub<INotificationSender>
{

}
