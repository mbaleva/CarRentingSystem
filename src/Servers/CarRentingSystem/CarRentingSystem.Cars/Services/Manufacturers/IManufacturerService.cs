namespace CarRentingSystem.Cars.Services.Manufacturers
{
    using CarRentingSystem.Cars.Data.Models;
    using CarRentingSystem.Cars.Models.Manufacturers;
    using System.Collections.Generic;

    public interface IManufacturerService
    {
        IEnumerable<ManufacturerModel> GetAll();
        Manufacturer GetByName(string name);
    }
}
