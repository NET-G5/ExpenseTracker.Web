
using ExpenseTracker.Web.Configurations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ExpenseTracker.Web.Services.Api;

public sealed class ApiClient : IApiClient
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpClient _client;

    public ApiClient(
        IHttpContextAccessor httpContextAccessor,
        HttpClient client,
        IOptions<ApiConfigurations> configurations)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _client.BaseAddress = new Uri(configurations.Value.BaseAddress);
    }

    public async Task DeleteAsync(string url)
    {
        AddToken();

        var response = await _client.DeleteAsync(url);
        response.EnsureSuccessStatusCode();
    }

    public async Task<TResult> GetAsync<TResult>(string url)
    {
        AddToken();

        var response = await _client.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<TResult>(json);

        if (result is null)
        {
            throw new InvalidOperationException("Could not deserialize result.");
        }

        return result;
    }

    public async Task<TResult> PostAsync<TResult, TRequest>(string url, TRequest request)
    {
        AddToken();

        var response = await _client.PostAsJsonAsync(url, request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<TResult>(json);

        if (result is null)
        {
            throw new InvalidOperationException("Could not deserialize result.");
        }

        return result;
    }

    public async Task PutAsync<TRequest>(string url, TRequest request)
    {
        AddToken();

        var response = await _client.PutAsJsonAsync(url, request);
        response.EnsureSuccessStatusCode();
    }

    private void AddToken()
    {
        var token = _httpContextAccessor.HttpContext?.Request?.Cookies["JWT"];

        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException();
        }

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
