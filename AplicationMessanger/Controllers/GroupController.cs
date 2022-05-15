using AplicationMessanger.Areas.Identity.Data;
using AplicationMessanger.Data;
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

        public GroupController(AplicationMessangerContext bd, UserManager<AplicationMessangerUser> userManager)
        {
            _bd = bd;
            _userManager = userManager;
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

        //// GET: GroupController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}
        [HttpPost]
        public IActionResult GetUsers([FromForm]ClassCreationVM? vm)
        {
            if (vm.SeacrhLine!=null)
            {
                vm.UsersFind = _userManager.Users.Where(u => u.UserName.Contains(vm.SeacrhLine)).ToList();
            }
            
            return View("Index",vm);
        }

        // GET: GroupController/Create
        public ActionResult Create(ClassCreationVM vm)
        {
            return View(vm);
        }

        // POST: GroupController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GroupController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GroupController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GroupController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GroupController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
