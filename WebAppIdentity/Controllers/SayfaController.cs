using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppIdentity.Controllers
{
    [Authorize]
    public class SayfaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }
    }
}
