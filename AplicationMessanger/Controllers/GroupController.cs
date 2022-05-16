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

        public GroupController(AplicationMessangerContext bd, UserManager<AplicationMessangerUser> userManager, IWebHostEnvironment webHostEnvironment)
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
        public ActionResult Post([FromForm]string UserId)
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
                vm.Users = _userManager.Users.Where(u => u.UserName.Contains(vm.SearchLine) && u.UserName != HttpContext.User.Identity.Name).ToList();
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

        //// GET: GroupController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: GroupController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
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

        //// GET: GroupController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

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
