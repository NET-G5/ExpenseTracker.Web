using ExpenseTracker.Web.Requests.Auth;

namespace ExpenseTracker.Web.Stores.Auth;

public interface IAuthStore
{
    Task RegisterAsync(RegisterUserRequest request);
    Task<bool> LoginAsync(LoginUserRequest request);
    Task EmailConfirmedAsync(EmailConfirmedRequest request);
    Task<bool> ResetPasswordAsync(ForgotPasswordRequest request);
    Task<bool> ConfirmResetPasswordAsync(ResetPasswordRequest request);
}
