using CarRentingSystem.Cars.Services.Manufacturers;
using CarRentingSystem.Cars.UnitTests.Helpers;
using CarRentingSystem.Cars.UnitTests.TestData;
using Shouldly;

namespace CarRentingSystem.Cars.UnitTests
{
    public class ManufacturerServiceTests
    {
        [Fact]
        public async Task Manufacturer_Service_Has_To_Return_All_Data_Correctly()
        {
            var dbContext = DbContextHelper.CreateDbContext();

            await dbContext.Manufacturers.AddRangeAsync(Manufacturers.GetManufacturers());
            await dbContext.SaveChangesAsync();

            var service = new ManufacturerService(dbContext);
            var result = service.GetAll();

            result.ShouldNotBeNull();
            result.Count().ShouldBe(50);
        }

        [Theory]
        [InlineData("Test 0")]
        [InlineData("Test 1")]
        [InlineData("Test 2")]
        public async Task Manufacturer_Service_Has_To_Get_Record_By_Name(string name) 
        {
            var dbContext = DbContextHelper.CreateDbContext();

            foreach (var item in Manufacturers.GetManufacturers())
            {
                await dbContext.Manufacturers.AddAsync(item);
            }
            await dbContext.Manufacturers.AddRangeAsync(Manufacturers.GetManufacturers());
            await dbContext.SaveChangesAsync();

            var service = new ManufacturerService(dbContext);

            var record = service.GetByName(name);

            record.ShouldNotBeNull();
            record.Name.ShouldBe(name);
        }
    }
}