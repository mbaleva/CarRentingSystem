using System.Linq;
using System.Threading.Tasks;
using Bogus;
using CarRentingSystem.Cars.Data;
using CarRentingSystem.Cars.Data.Models;
using CarRentingSystem.Cars.Models.Categories;
using CarRentingSystem.Cars.Services.Categories;
using CarRentingSystem.Cars.UnitTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace CarRentingSystem.Cars.UnitTests.Services.Categories
{
    public class CategoriesServiceTests
    {
        [Fact]
        public void FindByIdAsync_ReturnsCorrectCategoryModel()
        {
            // Arrange
            var dbContext = DbContextHelper.CreateDbContext();
            SeedTestData(dbContext);

            var service = new CategoriesService(dbContext);

            // Act
            var result = service.FindByIdAsync(1);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
            result.Name.ShouldBe("Category0");
            result.TotalCars.ShouldBe(2);
        }

        [Fact]
        public void GetCategory_ReturnsCorrectEditCategoryModel()
        {
            // Arrange
            var dbContext = DbContextHelper.CreateDbContext();
            SeedTestData(dbContext);

            var service = new CategoriesService(dbContext);

            // Act
            var result = service.GetCategory(1);

            // Assert
            result.ShouldNotBeNull();
            result.Name.ShouldBe("Category0");
        }

        [Fact]
        public async Task UpdateCategory_UpdatesCategorySuccessfully()
        {
            // Arrange
            var dbContext = DbContextHelper.CreateDbContext();
            SeedTestData(dbContext);

            var service = new CategoriesService(dbContext);
            var editModel = new EditCategoryModel { Name = "UpdatedCategory" };

            // Act
            await service.UpdateCategory(editModel, 1);
            var updatedCategory = dbContext.Categories.Find(1);

            // Assert
            updatedCategory.ShouldNotBeNull();
            updatedCategory.Name.ShouldBe("UpdatedCategory");
        }

        [Fact]
        public void GetAll_ReturnsCorrectCategoryModels()
        {
            // Arrange
            var dbContext = DbContextHelper.CreateDbContext();
            SeedTestData(dbContext);

            var service = new CategoriesService(dbContext);

            // Act
            var result = service.GetAll();

            // Assert
            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);
        }

        public static IEnumerable<object[]> TestCases()
        {
            yield return new object[] { "name" };
            yield return new object[] { string.Empty };
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        [Trait("Category", "DataDriven")]
        public async Task AddCategory_AddsCategorySuccessfully(string name)
        {
            // Arrange
            var dbContext = DbContextHelper.CreateDbContext();
            var service = new CategoriesService(dbContext);
            var addModel = new AddCategoryInputModel { Name = name };

            // Act
            await service.AddCategory(addModel);
            var addedCategory = dbContext.Categories.FirstOrDefault(c => c.Name == name);

            // Assert
            addedCategory.ShouldNotBeNull();
            addedCategory.Name.ShouldBe(name);
        }

        [Fact]
        public async Task DeleteById_DeletesCategorySuccessfully()
        {
            // Arrange
            var dbContext = DbContextHelper.CreateDbContext();
            SeedTestData(dbContext);

            var service = new CategoriesService(dbContext);

            // Act
            await service.DeleteById(1);
            var deletedCategory = dbContext.Categories.Find(1);

            // Assert
            deletedCategory.ShouldBeNull();
        }

        private void SeedTestData(ApplicationDbContext dbContext)
        {
            var categoryFaker = new Faker<Category>()
                .RuleFor(c => c.Id, f => f.IndexFaker + 1)
                .RuleFor(c => c.Name, f => "Category" + f.IndexVariable.ToString())
                .RuleFor(c => c.Cars, f => new Faker<Car>().Generate(2));

            var categories = categoryFaker.Generate(2);

            dbContext.Categories.AddRange(categories);
            dbContext.SaveChanges();
        }
    }
}
