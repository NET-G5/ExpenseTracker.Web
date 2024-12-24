using ExpenseTracker.Web.Requests.Category;
using ExpenseTracker.Web.ViewModels.Category;

namespace ExpenseTracker.Web.Stores.Category;

public interface ICategoryStore
{
    Task<List<CategoryViewModel>> GetAll(GetCategoriesRequest request);
    Task<CategoryViewModel> GetById(CategoryRequest request);
    Task<CategoryViewModel> Create(CreateCategoryRequest request);
    Task Update(UpdateCategoryRequest request);
    Task Delete(CategoryRequest request);
}
