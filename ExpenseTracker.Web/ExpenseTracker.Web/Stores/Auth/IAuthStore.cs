using Microsoft.AspNetCore.Identity.Data;

namespace ExpenseTracker.Web.Stores.Auth;

public interface IAuthStore
{
    Task RegisterAsync(RegisterRequest request);
    Task LoginAsync(LoginRequest request);
}
