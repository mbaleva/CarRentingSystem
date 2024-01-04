using System.Linq;
using Bogus;
using CarRentingSystem.Cars.Data;
using CarRentingSystem.Cars.Data.Models;
using CarRentingSystem.Cars.Models.Manufacturers;
using CarRentingSystem.Cars.Services.Manufacturers;
using CarRentingSystem.Cars.UnitTests.Fixtures;
using CarRentingSystem.Cars.UnitTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace CarRentingSystem.Cars.UnitTests.Services;

public class ManufacturerServiceTests : IClassFixture<ManufacturerServiceFixture>
{
    private readonly ManufacturerServiceFixture _fixture;

    public ManufacturerServiceTests(ManufacturerServiceFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void GetAll_ReturnsCorrectManufacturerModels()
    {
        var result = _fixture.ManufacturerService.GetAll();

        result.ShouldNotBeNull();
        result.Count().ShouldBe(2);

        var firstManufacturer = result.First();
        firstManufacturer.TotalCars.ShouldBe(3);

        var secondManufacturer = result.Skip(1).First();
        secondManufacturer.TotalCars.ShouldBe(3);
    }
}