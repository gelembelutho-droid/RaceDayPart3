using System.ComponentModel.DataAnnotations;

namespace RaceDay.MVC.ViewModels
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = "Participant"; // Organiser or Participant
        public DateTime? DateOfBirth { get; set; }
        public string? Phone { get; set; }
    }

    // Matches the response from your API
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public UserData User { get; set; } = new();
    }

    public class UserData
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}