using Microsoft.AspNetCore.Mvc;
using RaceDay.MVC.Middleware;
using RaceDay.MVC.Services;
using RaceDay.MVC.ViewModels;

namespace RaceDay.MVC.Controllers
{
    [RoleAuthorize("Participant")] // Secures the whole controller
    public class ParticipantController : Controller
    {
        private readonly IApiService _apiService;

        public ParticipantController(IApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: /Participant/MyRaces
        public async Task<IActionResult> MyRaces()
        {
            var token = HttpContext.Session.GetString("JWToken");
            // Call the API endpoint for My Registrations
            var regs = await _apiService.GetAsync<List<RegistrationDto>>("api/registrations/my-registrations", token);
            return View(regs ?? new List<RegistrationDto>());
        }

        // GET: /Participant/MyResults
        public async Task<IActionResult> MyResults()
        {
            var token = HttpContext.Session.GetString("JWToken");
            // Call the API endpoint for My Results
            var results = await _apiService.GetAsync<List<ResultDto>>("api/results/my-results", token);
            return View(results ?? new List<ResultDto>());
        }

        // POST: /Participant/Enrol
        [HttpPost]
        public async Task<IActionResult> Enrol(int raceId, int eventId)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var response = await _apiService.PostAsync<object, object>("api/registrations/enrol", new { raceId }, token);

            if (response == null)
            {
                TempData["Error"] = "Failed to enrol. You might already be enrolled.";
            }
            else
            {
                TempData["Success"] = "Successfully enrolled!";
            }

            return RedirectToAction("Details", "Home", new { id = eventId });
        }
    }
}