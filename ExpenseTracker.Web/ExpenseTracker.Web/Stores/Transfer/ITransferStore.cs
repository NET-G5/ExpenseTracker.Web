using ExpenseTracker.Web.Requests.Transfer;
using ExpenseTracker.Web.ViewModels.Transfer;

namespace ExpenseTracker.Web.Stores.Transfer;

public interface ITransferStore
{
    Task<List<TransferViewModel>> GetAll(GetTransfersRequest request);
    Task<TransferViewModel> GetById(TransferRequest request);
    Task<TransferViewModel> Create(CreateTransferRequest request, IEnumerable<IFormFile> attachments);
    Task Update(UpdateTransferRequest request);
    Task Delete(TransferRequest request);
}
