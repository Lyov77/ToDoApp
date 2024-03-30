using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoApp.Data.Context;
using ToDoApp.Data.Entities;
using ToDoApp.Models.Account;

namespace ToDoApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ToDoAppContext _context;

        public AccountController(ToDoAppContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoginAsync([Bind(Prefix = "l")] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new HomeViewModel
                {
                    LoginViewModel = model
                });
            }

            var user = await _context.User.FirstOrDefaultAsync(u => u.Login == model.Login &&
            u.Password == model.Password);

            if (user is null)
            {
                ViewBag.Error = "Wrong Login and/or Password";
                return View("Index", new HomeViewModel
                {
                    LoginViewModel = model
                });
            }

            await AuthenticateAsync(user);
            return RedirectToAction("Index", "Home");
        }

        public async Task AuthenticateAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> RegisterAsync([Bind(Prefix = "r")] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new HomeViewModel
                {
                    RegisterViewModel = model
                });
            }

            var user = await _context.User.FirstOrDefaultAsync(u => u.Login == model.Login);
            if (user != null)
            {
                ViewBag.RegisterError = "User with this Login already exists.";
                return View("Index", new HomeViewModel
                {
                    RegisterViewModel = model
                });
            }

            user = new User(model.Login, model.Password);
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            await AuthenticateAsync(user);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
