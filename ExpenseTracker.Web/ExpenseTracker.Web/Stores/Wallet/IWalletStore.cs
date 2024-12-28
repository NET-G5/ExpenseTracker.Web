using ExpenseTracker.Web.Requests.Wallet;
using ExpenseTracker.Web.Requests.WalletShare;
using ExpenseTracker.Web.ViewModels.Wallet;
using ExpenseTracker.Web.ViewModels.WalletShare;

namespace ExpenseTracker.Web.Stores.Wallet;

public interface IWalletStore
{
    Task<List<WalletViewModel>> GetAll(GetWalletsRequest request);
    Task<WalletViewModel> GetById(WalletRequest request);
    Task<WalletShareViewModel> GetWalletShareById(WalletShareRequest request);
    Task<WalletViewModel> Create(CreateWalletRequest request);
    Task Update(UpdateWalletRequest request);
    Task Delete(WalletRequest request);
    Task Share(CreateWalletShareRequest request);
}
