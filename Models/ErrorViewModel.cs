using System;
using System.Collections.Generic;

namespace RaceDay.MVC.ViewModels
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string? Location { get; set; }
        public string? City { get; set; }
        public string? Description { get; set; }
        public string? EventType { get; set; }
        public List<RaceDto> Races { get; set; } = new();
    }

    public class RaceDto
    {
        public int RaceId { get; set; }
        public string RaceName { get; set; } = string.Empty;
        public decimal DistanceKm { get; set; }
        public decimal Price { get; set; }
        public int? MaxParticipants { get; set; }
    }

    public class RegistrationDto
    {
        public int RegistrationId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string RaceName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? BibNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

    public class ResultDto
    {
        public int ResultId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string RaceName { get; set; } = string.Empty;
        public TimeSpan? FinishTime { get; set; }
        public int? OverallRank { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}