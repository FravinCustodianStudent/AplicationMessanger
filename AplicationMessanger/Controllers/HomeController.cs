using AplicationMessanger.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using AplicationMessanger.Areas.Identity.Data;
using AplicationMessanger.Data;
using AplicationMessanger.Models.Entity;
using AplicationMessanger.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AplicationMessanger.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AplicationMessangerContext _bd;
        private readonly UserManager<AplicationMessangerUser> _userManager;

        public HomeController(ILogger<HomeController> logger, AplicationMessangerContext _bd,UserManager<AplicationMessangerUser> userManager)
        {
            _logger = logger;
            this._bd = _bd;
            _userManager = userManager;
        }

        public IActionResult Index(int? ChatId)
        {
            _logger.LogInformation($"Index has been called with chatId {ChatId}");
            MainVM vm = new MainVM();
            vm.User = _bd.Users.FirstOrDefault(u => u.Id == _userManager.GetUserId(HttpContext.User));
            vm.Chats = _bd.Chats.Where(u => u.Users.Contains(vm.User))
                .ToList();
            if (ChatId!=null)
            {
                vm.ChatMessages = _bd.Messages.Where(u => u.ChatId == ChatId).ToList();
                
            }
            else
            {

                if (vm.Chats.Count>0)
                {
                    ChatId = vm.Chats.FirstOrDefault(x => x.Users.Contains(vm.User)).Id;
                    vm.ChatMessages = _bd.Messages.Where(u => u.ChatId == ChatId).ToList();
                }

            }

            _logger.LogInformation($"Index has created ViewModel with:\nUser:{vm.User}" +
                                   $"\nChats:{vm.Chats}" +
                                   $"\nMessages:{vm.ChatMessages}");

            return View(vm);
        }

        [HttpPost]
        async public void PostMassage([FromBody] Message message)
        {
            _bd.Add(message);
            _bd.SaveChangesAsync();
            
        }

        

        [HttpGet]
        public List<Chat> GetChats(string userId)
        {
            var user = _bd.Users.FirstOrDefault(e => e.Id == userId);
            var Chats = user.Chats;
            return Chats;

        }
        [HttpGet]
        public List<Message> GetMessages(int ChatId)
        {
            return _bd.Messages.Where(m => m.ChatId == ChatId).ToList(); ;
        }

        [HttpPost]
        public void PostChat([FromBody]Chat chat)
        {
            _bd.Chats.Add(chat);
            _bd.SaveChanges();
        }
         


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}