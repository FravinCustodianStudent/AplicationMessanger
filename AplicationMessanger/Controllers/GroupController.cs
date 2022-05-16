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
        public List<string> UserId { get; set; }

        public GroupController(AplicationMessangerContext bd, UserManager<AplicationMessangerUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _bd = bd;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }


        // GET: GroupController
        public ActionResult Index(ClassCreationVM? vm)
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
        public ActionResult Post([FromForm]ClassCreationVM vm)
        {
            
            vm.AlreadyIsinChat.Add(_userManager.Users.FirstOrDefault(u=> u.Id == vm.IdUser));
            return View("Create", vm);
        }

        //// GET: GroupController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}
        [HttpPost]
        public IActionResult GetUsers(ClassCreationVM? vm)
        {
            if (vm.SeacrhLine!=null)
            {
                vm.UsersFind = _userManager.Users.Where(u => u.UserName.Contains(vm.SeacrhLine) && u.UserName!=HttpContext.User.Identity.Name).ToList();
            }
            
            return View("Index",vm);
        }


        // GET: GroupController/Create
        [HttpGet]

        public ActionResult Create(ClassCreationVM vm)
        {
            return View(vm);
        }

        // POST: GroupController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateChat(ClassCreationVM potentialChat)
        {
            
                var chat = new Chat();
                chat.Name = potentialChat.CurrentChat.Name;
                for (int i = 0; i < potentialChat.UsersNames.Count; i++)
                {
                    var user = _userManager.Users.FirstOrDefault(u => u.UserName == potentialChat.UsersNames[i]);
                    chat.Users.Add(user);
                }
                chat.Users.Add(_bd.Users.FirstOrDefault(u => u.Id == _userManager.GetUserId(HttpContext.User)));
                string webRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString();
                var extensions = Path.GetExtension(potentialChat.Avatar.Name);
                var filePath = Path.Combine(webRootPath + WC.ImagePath, fileName+extensions);
                using (var stream = new FileStream(filePath,FileMode.Create))
                {
                    potentialChat.Avatar.CopyTo(stream);
                }

                chat.Avatar = fileName+extensions;
                _bd.Chats.Add(chat);
                _bd.SaveChanges();
                
            
            return RedirectToAction("Index","Home");
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
