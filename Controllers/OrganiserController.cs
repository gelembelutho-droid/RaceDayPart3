using Microsoft.AspNetCore.Mvc;
using RaceDay.MVC.Middleware;
using RaceDay.MVC.Services;
using RaceDay.MVC.ViewModels;

namespace RaceDay.MVC.Controllers
{
    [RoleAuthorize("Organiser")] // Only Organisers can access this!
    public class OrganiserController : Controller
    {
        private readonly IApiService _apiService;

        public OrganiserController(IApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: /Organiser/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var token = HttpContext.Session.GetString("JWToken");
            // Fetch only the events created by this Organiser
            var events = await _apiService.GetAsync<List<EventDto>>("api/events/my-events", token);
            return View(events ?? new List<EventDto>());
        }

        // GET: /Organiser/CreateEvent
        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var token = HttpContext.Session.GetString("JWToken");
            string? bannerUrl = null;

       

            // 2. Create the Event using the DTO
            var createDto = new
            {
                EventName = model.EventName,
                EventDate = model.EventDate,
                Location = model.Location,
                City = model.City,
                Country = model.Country,
                Description = model.Description,
                EventType = model.EventType,
                BannerImageUrl = bannerUrl // Pass the URL from Azure
            };

            var success = await _apiService.PostAsync<object, object>("api/events", createDto, token);

            if (success != null)
            {
                TempData["Success"] = "Event created successfully!";
                return RedirectToAction("Dashboard");
            }

            TempData["Error"] = "Failed to create event.";
            return View(model);
        }
    }
}