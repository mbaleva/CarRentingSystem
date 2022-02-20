namespace CarRentingSystem.Cars.Services.Categories
{
    using CarRentingSystem.Cars.Data;
    using CarRentingSystem.Cars.Data.Models;
    using CarRentingSystem.Cars.Models.Categories;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext dbContext;

        public CategoriesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public CategoryModel FindByIdAsync(int id)
        {
            return this.dbContext.Categories.Where(x => x.Id == id)
                .Select(x => new CategoryModel { Id = x.Id, Name = x.Name, TotalCars = x.Cars.Count })
                .FirstOrDefault();
        }
        public EditCategoryModel GetCategory(int id)
        {
            return this.dbContext.Categories.Where(x => x.Id == id)
                .Select(x => new EditCategoryModel { Name = x.Name })
                .FirstOrDefault();
        }
        public async Task UpdateCategory(EditCategoryModel model, int id)
        {
            var category = this.dbContext.Categories.Where(x => x.Id == id)
                .FirstOrDefault();
            category.Name = model.Name;
            await this.dbContext.SaveChangesAsync();
        }
        public IEnumerable<CategoryModel> GetAll(int page, int itemsPerPage)
        {
            return this.dbContext.Categories
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new CategoryModel { Name = x.Name, TotalCars = x.Cars.Count, Id = x.Id })
                .ToList();
        }
        public async Task AddCategory(AddCategoryInputModel model)
        {
            Category category = new Category 
            {
                Name = model.Name,
            };
            await this.dbContext.Categories.AddAsync(category);
            await this.dbContext.SaveChangesAsync();
        }
        public async Task DeleteById(int id)
        {
            var category = this.dbContext.Categories.Where(x => x.Id == id).FirstOrDefault();
            this.dbContext.Categories.Remove(category);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
