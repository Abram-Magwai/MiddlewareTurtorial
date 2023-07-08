using Microsoft.AspNetCore.Mvc;
using MiddlewareTurtorial.Models;

namespace MiddlewareTurtorial.Controllers
{
    public class AccountController : Controller
    {
        [BindProperty]
        public Login Login { get; set; } = null!;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignIn(Login login)
        {
            if (!ModelState.IsValid) return View(login);
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}
