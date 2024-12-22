using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Web.Requests.Auth;

public sealed class RegisterUserRequest
{
    [Required(ErrorMessage = "Username is required")]
    [MinLength(3, ErrorMessage = "Username must have at least 3 characters")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must have at least 8 characters")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare(nameof(Password), ErrorMessage = "Passwords must match")]
    public required string ConfirmPassword { get; set; }

    [Phone(ErrorMessage = "Invalid phone number")]
    public string? PhoneNumber { get; set; }
    public string? ConfirmUrl { get; set; }
    public string? Browser { get; set; }
    public string? OS { get; set; }

}