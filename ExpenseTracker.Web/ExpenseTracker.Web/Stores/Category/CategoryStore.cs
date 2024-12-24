using ExpenseTracker.Web.Requests.Category;
using ExpenseTracker.Web.Services.Api;
using ExpenseTracker.Web.ViewModels.Category;

namespace ExpenseTracker.Web.Stores.Category;

public class CategoryStore : ICategoryStore
{
    private readonly IApiClient _apiClient;
    public CategoryStore(IApiClient apiClient)
    {
        _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
    }
    public async Task<CategoryViewModel> Create(CreateCategoryRequest request)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        var response = await _apiClient.PostAsync<CategoryViewModel, CreateCategoryRequest>("categories", request);

        return response;
    }

    public async Task Delete(CategoryRequest request)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        await _apiClient.DeleteAsync($"categories/{request.Id}");

    }

    public async Task<List<CategoryViewModel>> GetAll(GetCategoriesRequest request)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        var response = await _apiClient.GetAsync<List<CategoryViewModel>>("categories");

        return response;
    }

    public async Task<CategoryViewModel> GetById(CategoryRequest request)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        var response = await _apiClient.GetAsync<CategoryViewModel>($"categories/{request.Id}");

        return response;
    }

    public async Task Update(UpdateCategoryRequest request)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        await _apiClient.PutAsync($"categories/{request.Id}", request);
    }
}
