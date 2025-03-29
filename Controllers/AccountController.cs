using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace contactNumbersManager.Controllers
{
    public class AccountController : Controller
    {
        // Hardcoded users (for demo purposes)
        private readonly Dictionary<string, string> users = new Dictionary<string, string>
        {
            { "user1", "user1" }, // username: user1, password: user1
            { "user2", "user2" }  // username: user2, password: user1
        };

        public IActionResult Index()
        {
            return RedirectToAction("Login");

        }

        // GET: Login page
        [HttpGet]
        public IActionResult Login() => View();

        // POST: Handle login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Validate user credentials
            if (users.ContainsKey(username) && users[username] == password)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Sign in the user
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Contacts"); // Redirect to home page after login
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }

        // POST: Handle logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // Access Denied Action
        public IActionResult AccessDenied() => View();
    }
}
