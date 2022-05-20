using AplicationMessanger.Areas.Identity.Data;
using AplicationMessanger.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AplicationMessanger.Controllers
{
    public class AccountController : Controller
    {
        private readonly AplicationMessangerContext _bd;
        private readonly UserManager<AplicationMessangerUser> _userManager;

        public AccountController(AplicationMessangerContext bd, UserManager<AplicationMessangerUser> userManager)
        {
            _bd = bd;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
