using Microsoft.AspNetCore.SignalR;

namespace AplicationMessanger.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessageToChat(string chatId,string message)
        {
            return Clients.Group(chatId).SendAsync("ReceiveMessage",message);
        }

        public Task JoinGroup(string chatId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }
    }
}
