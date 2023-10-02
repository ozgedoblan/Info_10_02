using WebAppIdentity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppIdentity.Models.Identities;

namespace WebAppIdentity.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterUserModel model)
        {
            var newUser = new AppUser
            {
                Fullname = model.Fullname,
                Email = model.Email,
                UserName = model.Username,
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("username", "XXXXX");
                ModelState.AddModelError("", "YYYYY");

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            else
            {
                var adminRole = await _roleManager.FindByNameAsync("Admin");
                if (adminRole != null)
                {
                    IdentityResult roleresult = await _userManager.AddToRoleAsync(newUser, adminRole.Name);
                }
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                //ModelState.AddModelError("username", "Kullanıcı bulunamadı");
                ModelState.AddModelError("", "Kullanıcı ve/veya şifre yanlış");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
            {
                //ModelState.AddModelError("password", "yanlış şifre");
                ModelState.AddModelError("", "Kullanıcı ve/veya şifre yanlış");
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task Logout() => await _signInManager.SignOutAsync();
    }
}
