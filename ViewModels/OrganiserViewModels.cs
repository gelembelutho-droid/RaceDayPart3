namespace RaceDay.MVC.ViewModels
{
    public class CreateEventViewModel
    {
        public string EventName { get; set; } = string.Empty;
        public DateTime EventDate { get; set; } = DateTime.Now.AddMonths(1);
        public string Location { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = "South Africa";
        public string Description { get; set; } = string.Empty;
        public string EventType { get; set; } = "Run";

        // This is the file the Organiser uploads
        public IFormFile? BannerImage { get; set; }
    }
}