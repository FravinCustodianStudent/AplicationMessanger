using AplicationMessanger.Areas.Identity.Data;
using AplicationMessanger.Data;
using AplicationMessanger.Models.Entity;
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
            var chat = _bd.Chats.FirstOrDefault(c => c.Id == chatId);
            var Message = new Message();
            Message.Chat = chat;
            Message.ChatId = int.Parse(chatId);
            Message.Content = message;
            Message.Id = Guid.NewGuid().ToString();
            Message.Time = now;
            Message.User = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            _bd.Messages.Add(Message);
            _bd.SaveChanges();
            return Clients.Group(chatId).SendAsync("ReceiveMessage",message,time,userId,pathToImage,userName);
        }

        public Task JoinGroup(string chatId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }
    }
}
