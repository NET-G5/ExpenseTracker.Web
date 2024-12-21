using ExpenseTracker.Web.Configurations;
using ExpenseTracker.Web.Stores.Auth;
using ExpenseTracker.Web.ViewModels.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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

    public async Task LoginAsync(LoginRequest request)
    {
        var response = await _client.PostAsJsonAsync("/auth/login", request);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<LoginViewModel>(json);

        if (result is null)
        {
            throw new InvalidOperationException("Could not deserialize Login response.");
        }

        _httpContextAccessor.HttpContext?.Response?.Cookies?.Append("JWT", result.Token);
    }

    public Task RegisterAsync(RegisterRequest request)
    {
        throw new NotImplementedException();
    }
}
