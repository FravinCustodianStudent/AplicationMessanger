using AplicationMessanger.Areas.Identity.Data;
using AplicationMessanger.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace AplicationMessanger.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AplicationMessangerContext _bd;
        private readonly UserManager<AplicationMessangerUser> _userManager;
        public ChatHub(AplicationMessangerContext bd, UserManager<AplicationMessangerUser> userManager)
        {
            _bd = bd;
            _userManager = userManager;
        }
        public Task SendMessageToChat(string chatId,string message,string date,string userId)
        {
            DateTime now = DateTime.Parse(date);
            var User = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            string pathToImage = User.Avatar;
            string userName = User.UserName;
            string time = now.ToString("MM/dd/yyyy HH:mm");
            return Clients.Group(chatId).SendAsync("ReceiveMessage",message,time,userId,pathToImage,userName);
        }

        public Task JoinGroup(string chatId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }
    }
}
