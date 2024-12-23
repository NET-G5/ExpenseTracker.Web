using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Web.Requests.Auth;

public sealed record LoginUserRequest(
    [Required(ErrorMessage = ("Username is required"))]
    string UserName,
    [Required(ErrorMessage = ("Password is required"))]
    string Password);
