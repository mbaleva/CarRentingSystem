using Bogus;
using CarRentingSystem.Cars.Data;
using CarRentingSystem.Cars.Data.Models;
using CarRentingSystem.Cars.Services.Manufacturers;
using CarRentingSystem.Cars.UnitTests.Helpers;

namespace CarRentingSystem.Cars.UnitTests.Fixtures;

public class ManufacturerServiceFixture : IDisposable
{
    public ApplicationDbContext DbContext { get; private set; }
    public ManufacturerService ManufacturerService { get; private set; }

    public ManufacturerServiceFixture()
    {
        DbContext = DbContextHelper.CreateDbContext();
        SeedTestData(DbContext);
        ManufacturerService = new ManufacturerService(DbContext);
    }

    public void Dispose()
    {
        DbContext.Dispose();
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
