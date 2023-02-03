namespace CarRentingSystem.Cars.Services.Manufacturers
{
    using CarRentingSystem.Cars.Data;
    using CarRentingSystem.Cars.Data.Models;
    using CarRentingSystem.Cars.Models.Manufacturers;
    using System.Collections.Generic;
    using System.Linq;

    public class ManufacturerService : IManufacturerService
    {
        private readonly ApplicationDbContext dbContext;

        public ManufacturerService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<ManufacturerModel> GetAll()
        {
            return this.dbContext.Manufacturers
                .Select(x => new ManufacturerModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    TotalCars = x.Cars.Count,
                }).ToList();
        }

        public Manufacturer GetByName(string name)
        {
            return this.dbContext.Manufacturers.Where(x => x.Name == name)
                .FirstOrDefault();
        }
    }
}
