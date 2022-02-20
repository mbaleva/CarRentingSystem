namespace CarRentingSystem.Cars.Services.Manufacturers
{
    using CarRentingSystem.Cars.Data.Models;
    public interface IManufacturerService
    {
        Manufacturer GetByName(string name);
    }
}
