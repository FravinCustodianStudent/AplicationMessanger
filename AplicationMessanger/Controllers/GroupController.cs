using AplicationMessanger.Areas.Identity.Data;
using AplicationMessanger.Data;
using AplicationMessanger.Models;
using AplicationMessanger.Models.Entity;
using AplicationMessanger.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AplicationMessanger.Controllers
{



    [Authorize]
    public class GroupController : Controller
    {
        private readonly AplicationMessangerContext _bd;
        private readonly UserManager<AplicationMessangerUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GroupController(AplicationMessangerContext bd, UserManager<AplicationMessangerUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _bd = bd;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }


        // GET: GroupController
        public ActionResult Index(UserFormVM? vm)
        {
            if (vm == null)
            {
                return View();
            }
            else
            {
                return View(vm);
            }

        }

        [HttpPost]
        public ActionResult Post([FromForm] string UserId)
        {


            ChatVM vm = new ChatVM();
            vm.UserId = UserId;

            return View("Create", vm);
        }

        //// GET: GroupController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}
        [HttpPost]
        public IActionResult GetUsers(UserFormVM? vm)
        {
            if (vm.SearchLine != null)
            {
                vm.Users = _userManager.Users.Where(u =>
                    u.UserName.Contains(vm.SearchLine) && u.UserName != HttpContext.User.Identity.Name).ToList();
            }

            return View("Index", vm);
        }


        // GET: GroupController/Create
        [HttpGet]

        public ActionResult Create(ChatVM? vm)
        {
            return View(vm);
        }

        // POST: GroupController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateChat(ChatVM potentialChat)
        {

            var Newchat = new Chat();
            if (ModelState.IsValid)
            {
                Newchat.Name = potentialChat.ChatName;
                //for (int i = 0; i < chat.UsersId.Count; i++)
                //{
                //    var user = _userManager.Users.FirstOrDefault(u => u.UserName == chat.UsersId[i]);
                //    Newchat.Users.Add(user);
                //}

                var user = _userManager.Users.FirstOrDefault(u => u.Id == potentialChat.UserId);
                Newchat.Users.Add(user);
                Newchat.Users.Add(_bd.Users.FirstOrDefault(u => u.Id == _userManager.GetUserId(HttpContext.User)));
                string webRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString();
                var extensions = Path.GetExtension(potentialChat.Avatar.FileName);
                var filePath = Path.Combine(webRootPath + WC.ImagePath, fileName + extensions);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    potentialChat.Avatar.CopyTo(stream);
                }

                Newchat.Avatar = fileName + extensions;
                var Chats = _bd.Chats.ToList();
                Newchat.Id = (Chats.Count + 1).ToString();
                _bd.Chats.Add(Newchat);
                _bd.SaveChanges();
            }
            else
            {
                return View("Create", potentialChat);
            }



            return RedirectToAction("Index", "Home");
        }

        //GET: GroupController/Edit/5
        public ActionResult Edit(string id)
        {
            ChatChangeVM vm = new ChatChangeVM();
            vm.ChatId = id;
            vm.CurrentChat = _bd.Chats.FirstOrDefault(c => c.Id == id);
            vm.CurrentChat.Users = _userManager.Users
                .Where(u => u.Chats.Contains(_bd.Chats.FirstOrDefault(c => c.Id == id))).ToList();
            return View(vm);
        }

        [HttpPost]

        public IActionResult Edit(ChatChangeVM chatForUpdate)
        {
            if (ModelState.IsValid)
            {
                chatForUpdate.CurrentChat = _bd.Chats.FirstOrDefault(c => c.Id == chatForUpdate.ChatId);
                string webRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString();
                var extensions = Path.GetExtension(chatForUpdate.NewAvatar.FileName);

                var oldFile = Path.Combine(webRootPath + WC.ImagePath, chatForUpdate.CurrentChat.Avatar);

                var filePath = Path.Combine(webRootPath + WC.ImagePath, fileName + extensions);
                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    chatForUpdate.NewAvatar.CopyTo(stream);
                }

                var CurrentChat = chatForUpdate.CurrentChat;
                CurrentChat.Name = chatForUpdate.Name;
                CurrentChat.Avatar = fileName + extensions;

                _bd.Chats.Update(CurrentChat);
                _bd.SaveChanges();


            }
            else
            {
                return View(chatForUpdate);
            }

            return RedirectToAction("Index", "Home", chatForUpdate.ChatId);



        }

        [HttpGet]
        public IActionResult AddUsers(string Id)
        {

            AddUserVM vm = new AddUserVM();
            var Chat = _bd.Chats.FirstOrDefault(c => c.Id == Id);

            vm.ChatId = Chat.Id;
            vm.CurrentChat = Chat;

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddUsers(AddUserVM vm)
        {
            var Chat = _bd.Chats.FirstOrDefault(c => c.Id == vm.ChatId);
            var User = _userManager.Users.FirstOrDefault(u => u.Id == vm.UserId);
            Chat.Users.Add(User);
            _bd.Chats.Update(Chat);
            _bd.SaveChanges();

            return RedirectToAction("Edit", new {id = Chat.Id});
        }

        public IActionResult GetUsersForUpdate(AddUserVM vm)
        {

            vm.CurrentChat = _bd.Chats.FirstOrDefault(c => c.Id == vm.ChatId);
            if (vm.SearchLine != "")
            {

                vm.Users = _userManager.Users
                    .Where(u => u.UserName.Contains(vm.SearchLine) && !u.Chats.Contains(vm.CurrentChat)).ToList();
            }

            return View("AddUsers", vm);
        }


        // GET: GroupController/Delete/5
        [HttpGet]
        public ActionResult DeleteUser(string Id)
        {
            DeleteUserVM vm = new DeleteUserVM();
            vm.ChatId = Id;
            var Chat = _bd.Chats.FirstOrDefault(c => c.Id == Id);
            vm.Users = _userManager.Users.Where(u => u.Chats.Contains(Chat)).ToList();
            return View(vm);
        }
        [HttpPost]
        public ActionResult DeleteUser(DeleteUserVM vm)
        {
            var Chat = _bd.Chats.FirstOrDefault(c => c.Id == vm.ChatId);
            var User = _userManager.Users.FirstOrDefault(u => u.Id == vm.UserId);
            Chat.Users = _userManager.Users.Where(u => u.Chats.Contains(Chat)).ToList();
            Chat.Messages = _bd.Messages.Where(m => m.Chat.Id == Chat.Id).ToList();
            _bd.Chats.Remove(Chat);
            _bd.SaveChanges();

            Chat.Users.Remove(User);
            _bd.Chats.Add(Chat);
            _bd.SaveChanges();
            return RedirectToAction("Edit", new { id = Chat.Id });
        }

        //// POST: GroupController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
