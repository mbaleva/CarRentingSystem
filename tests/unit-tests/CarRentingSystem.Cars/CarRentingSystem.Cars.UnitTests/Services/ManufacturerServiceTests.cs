using System.Linq;
using Bogus;
using CarRentingSystem.Cars.Data;
using CarRentingSystem.Cars.Data.Models;
using CarRentingSystem.Cars.Models.Manufacturers;
using CarRentingSystem.Cars.Services.Manufacturers;
using CarRentingSystem.Cars.UnitTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace CarRentingSystem.Cars.UnitTests.Services
{
    public class ManufacturerServiceTests
    {
        [Fact]
        public void GetAll_ReturnsCorrectManufacturerModels()
        {
            // Arrange
            var dbContext = DbContextHelper.CreateDbContext();
            SeedTestData(dbContext);

            var service = new ManufacturerService(dbContext);

            // Act
            var result = service.GetAll();

            // Assert
            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);

            var firstManufacturer = result.First();
            firstManufacturer.TotalCars.ShouldBe(3);

            var secondManufacturer = result.Skip(1).First();
            secondManufacturer.TotalCars.ShouldBe(3);
        }

        [Fact]
        public async void GetByName_ReturnsCorrectManufacturer()
        {
            // Arrange
            var dbContext = DbContextHelper.CreateDbContext();
            dbContext.Manufacturers.Add(new Manufacturer
            {
                Id = 4,
                Name = "Manufacturer1"
            });

            await dbContext.SaveChangesAsync();

            var service = new ManufacturerService(dbContext);

            // Act
            var result = service.GetByName("Manufacturer1");

            // Assert
            result.ShouldNotBeNull();
            result.Name.ShouldBe("Manufacturer1");
        }

        private void SeedTestData(ApplicationDbContext dbContext)
        {
            var manufacturerFaker = new Faker<Manufacturer>()
                .RuleFor(m => m.Id, f => f.IndexFaker + 1)
                .RuleFor(m => m.Name, f => f.Company.CompanyName())
                .RuleFor(m => m.Cars, f => new Faker<Car>().Generate(3));

            var manufacturers = manufacturerFaker.Generate(2);

            dbContext.Manufacturers.AddRange(manufacturers);
            dbContext.SaveChanges();
        }
    }
}
