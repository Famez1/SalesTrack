using Microsoft.AspNetCore.SignalR;

namespace SalesTrack.Abstractions.Infrastructure;

public interface INotificationSender
{
    [HubMethodName("noneExpired")]
    public Task NoteExpired(string Content);
}
