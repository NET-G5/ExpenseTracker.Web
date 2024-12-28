using ExpenseTracker.Web.Requests.Wallet;
using ExpenseTracker.Web.Requests.WalletShare;
using ExpenseTracker.Web.Services.Api;
using ExpenseTracker.Web.ViewModels.Wallet;
using ExpenseTracker.Web.ViewModels.WalletShare;

namespace ExpenseTracker.Web.Stores.Wallet;

internal sealed class WalletStore : IWalletStore
{
    private readonly IApiClient _apiClient;

    public WalletStore(IApiClient apiClient)
    {
        _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
    }
    public async Task<List<WalletViewModel>> GetAll(GetWalletsRequest request)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        var response = await _apiClient.GetAsync<List<WalletViewModel>>($"wallets?Search={request.Search}");

        return response;
    }

    public async Task<WalletViewModel> GetById(WalletRequest request)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        var response = await _apiClient.GetAsync<WalletViewModel>($"wallets/{request.Id}");

        return response;
    }
    public async Task<WalletViewModel> Create(CreateWalletRequest request)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        var isSuccess = await _apiClient.PostAsync<WalletViewModel, CreateWalletRequest>("wallets", request);

        return isSuccess;
    }
    public async Task Update(UpdateWalletRequest request)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        await _apiClient.PutAsync($"wallets/{request.Id}", request);

    }
    public async Task Delete(WalletRequest request)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        await _apiClient.DeleteAsync($"wallets/{request.Id}");
    }

    public Task<WalletShareViewModel> GetWalletShareById(WalletShareRequest request)
    {
        throw new NotImplementedException();
    }

    public Task Share(CreateWalletShareRequest request)
    {
        throw new NotImplementedException();
    }
}
