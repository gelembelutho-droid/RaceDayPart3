using Microsoft.AspNetCore.Mvc;
using RaceDay.MVC.Services;
using RaceDay.MVC.ViewModels;

namespace RaceDay.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApiService _apiService;

        public AccountController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var response = await _apiService.PostAsync<object, AuthResponse>("api/auth/login", new { model.Email, model.Password });

                if (response == null || string.IsNullOrEmpty(response.Token))
                {
                    TempData["Error"] = "API rejected the login. Check if the email and password are correct.";
                    return View(model);
                }

                // Save Auth Data to Session
                HttpContext.Session.SetString("JWToken", response.Token);
                HttpContext.Session.SetString("UserRole", response.User.Role);
                HttpContext.Session.SetString("UserName", response.User.FullName);

                // Redirect based on role
                if (response.User.Role == "Organiser") return RedirectToAction("Dashboard", "Organiser");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Cannot connect to API: {ex.Message}";
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var response = await _apiService.PostAsync<RegisterViewModel, AuthResponse>("api/auth/register", model);

                if (response == null || string.IsNullOrEmpty(response.Token))
                {
                    TempData["Error"] = "Registration failed. Email might already exist.";
                    return View(model);
                }

                HttpContext.Session.SetString("JWToken", response.Token);
                HttpContext.Session.SetString("UserRole", response.User.Role);
                HttpContext.Session.SetString("UserName", response.User.FullName);

                if (response.User.Role == "Organiser") return RedirectToAction("Dashboard", "Organiser");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Cannot connect to API: {ex.Message}";
                return View(model);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied() => View();
    }
}