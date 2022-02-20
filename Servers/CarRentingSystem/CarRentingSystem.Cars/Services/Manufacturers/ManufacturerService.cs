namespace CarRentingSystem.Cars.Services.Manufacturers
{
    using CarRentingSystem.Cars.Data;
    using CarRentingSystem.Cars.Data.Models;
    using System.Linq;

    public class ManufacturerService : IManufacturerService
    {
        private readonly ApplicationDbContext dbContext;

        public ManufacturerService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Manufacturer GetByName(string name)
        {
            return this.dbContext.Manufacturers.Where(x => x.Name == name)
                .FirstOrDefault();
        }
    }
}
