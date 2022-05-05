using AplicationMessanger.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using AplicationMessanger.Data;
using AplicationMessanger.Models.Entity;
using Microsoft.AspNetCore.Authorization;

namespace AplicationMessanger.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AplicationMessangerContext _bd;

        public HomeController(ILogger<HomeController> logger, AplicationMessangerContext _bd)
        {
            _logger = logger;
            this._bd = _bd;
        }

        public IActionResult Index()
        {
            return View();
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