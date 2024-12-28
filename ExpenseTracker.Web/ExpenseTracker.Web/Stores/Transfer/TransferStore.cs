using ExpenseTracker.Web.Requests.Transfer;
using ExpenseTracker.Web.Services.Api;
using ExpenseTracker.Web.ViewModels.Transfer;

namespace ExpenseTracker.Web.Stores.Transfer;

public class TransferStore : ITransferStore
{
    private readonly IApiClient _apiClient;
    public TransferStore(IApiClient apiClient)
    {
        _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
    }

    public async Task<List<TransferViewModel>> GetAll(GetTransfersRequest request)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        var response = await _apiClient.GetAsync<List<TransferViewModel>>($"transfers?search={request.Search}");

        return response;
    }

    public async Task<TransferViewModel> GetById(TransferRequest request)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        var response = await _apiClient.GetAsync<TransferViewModel>($"transfers/{request.Id}");

        return response;
    }
    public async Task<TransferViewModel> Create(CreateTransferRequest request, IEnumerable<IFormFile> attachments)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        var response = await _apiClient.PostAsync<TransferViewModel, CreateTransferRequest>("transfers", request);

        return response;

    }

    public async Task Update(UpdateTransferRequest request)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        await _apiClient.PutAsync($"transfers/{request.Id}", request);

    }
    public async Task Delete(TransferRequest request)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        await _apiClient.DeleteAsync($"transfers/{request.Id}");

    }
}
