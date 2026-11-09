using Microsoft.AspNetCore.Mvc;
using RaceDay.MVC.Services;
using RaceDay.MVC.ViewModels;

namespace RaceDay.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiService _apiService;

        public HomeController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Fetch all events from API
                var events = await _apiService.GetAsync<List<EventDto>>("api/events");

                // If API returns null (e.g. API is offline), give it an empty list so the page doesn't crash
                return View(events ?? new List<EventDto>());
            }
            catch (Exception ex)
            {
                // This will show the error on the screen if the API connection fails
                ViewBag.Error = "Could not connect to the API: " + ex.Message;
                return View(new List<EventDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var ev = await _apiService.GetAsync<EventDto>($"api/events/{id}");
            if (ev == null) return NotFound();
            return View(ev);
        }
    }
}