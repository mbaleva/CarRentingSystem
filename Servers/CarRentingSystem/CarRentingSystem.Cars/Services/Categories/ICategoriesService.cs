namespace CarRentingSystem.Cars.Services.Categories
{
    using CarRentingSystem.Cars.Models.Categories;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        CategoryModel FindByIdAsync(int id);
        EditCategoryModel GetCategory(int id);
        Task UpdateCategory(EditCategoryModel model, int id);
        IEnumerable<CategoryModel> GetAll(int page, int itemsPerPage);
        Task AddCategory(AddCategoryInputModel model);
        Task DeleteById(int id);
    }
}
