using ExpenseTracker.Web.Configurations;
using ExpenseTracker.Web.Requests.Auth;
using ExpenseTracker.Web.Stores.Auth;
using Microsoft.Extensions.Options;

namespace ExpenseTracker.Web.Stores;

internal sealed class AuthStore : IAuthStore
{
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthStore(
        HttpClient client,
        IOptions<ApiConfigurations> configurations,
        IHttpContextAccessor httpContextAccessor)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _client.BaseAddress = new Uri(configurations.Value.BaseAddress);
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> LoginAsync(LoginUserRequest request)
    {
        var response = await _client.PostAsJsonAsync("auth/login", request);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var token = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(token))
        {
            throw new InvalidOperationException("Could not deserialize Login response.");
        }

        _httpContextAccessor.HttpContext?.Response?.Cookies?.Append("JWT", token);

        return true;
    }

    public async Task RegisterAsync(RegisterUserRequest request)
    {
        var response = await _client.PostAsJsonAsync("auth/register", request);
        var json = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();
    }

    public async Task EmailConfirmedAsync(EmailConfirmedRequest request)
    {
        var response = await _client.PostAsJsonAsync("auth/confirm-email", request);

        response.EnsureSuccessStatusCode();
    }
}
