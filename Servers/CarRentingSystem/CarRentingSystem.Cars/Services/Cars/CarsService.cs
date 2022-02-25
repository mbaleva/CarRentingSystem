namespace CarRentingSystem.Cars.Services.Cars
{
    using CarRentingSystem.Cars.Data;
    using CarRentingSystem.Cars.Data.Models;
    using CarRentingSystem.Cars.Models.Cars;
    using CarRentingSystem.Cars.Models.Dealers;
    using CarRentingSystem.Cars.Services.Categories;
    using CarRentingSystem.Cars.Services.Dealers;
    using CarRentingSystem.Cars.Services.Manufacturers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MassTransit;
    using CarRentingSystem.Common.Messages;

    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IManufacturerService manufacturer;
        private readonly IDealersService dealers;
        private readonly ICategoriesService categories;
        private readonly IBus bus;

        private const int CARS_PER_PAGE = 1500;

        public CarsService(
            ApplicationDbContext dbContext,
            IManufacturerService manufacturer,
            IDealersService dealers,
            ICategoriesService categories,
            IBus bus)
        {
            this.dbContext = dbContext;
            this.manufacturer = manufacturer;
            this.dealers = dealers;
            this.categories = categories;
            this.bus = bus;
        }
        public async Task<int> AddAsync(AddCarInputModel model, string userId)
        {
            var dealerId = this.dealers.GetDealerIdByUser(userId);
            var manufacturer = this.manufacturer.GetByName(model.ManufacturerName);

            if (manufacturer == null)
            {
                manufacturer = new Manufacturer { Name = model.ManufacturerName };
            }
            var car = new Car
            {
                CarName = model.Name,
                CategoryId = model.CategoryId,
                DealerId = dealerId,
                ImageUrl = model.ImageUrl,
                Manufacturer = manufacturer,
                PricePerDay = model.PricePerDay,
                Model = model.Model,
                HasAutomaticTransmission = model.HasAutomaticTransmission,
                HasClimateControl = model.HasClimateControl,
                SeatsCount = model.SeatsCount,
            };
            await this.dbContext.Cars.AddAsync(car);
            await this.dbContext.SaveChangesAsync();

            var message = new CarCreatedMessage();
            await this.bus.Publish(message);

            return car.Id;
        }
        public async Task<bool> DeleteById(int id)
        {
            var car = this.dbContext.Cars.Where(x => x.Id == id).FirstOrDefault();
            if (car == null)
            {
                return false;
            }
            this.dbContext.Cars.Remove(car);
            await this.dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<CarByIdModel> GetCarById(int id, string userId = null)
        {

            var car = this.dbContext.Cars.Where(x => x.Id == id)
                .Select(x => new CarByIdModel
                {
                    Name = x.CarName,
                    Category = x.Category.Name,
                    Dealer = new ById
                    {
                        Id = x.Dealer.Id,
                        Name = x.Dealer.FullName,
                        PhoneNumber = x.Dealer.PhoneNumber,
                        TotalCars = x.Dealer.Cars.Count
                    },
                    PricePerDay = x.PricePerDay,
                    HasClimateControl = x.HasClimateControl,
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    IsAvailable = x.IsAvailable,
                    Manufacturer = x.Manufacturer.Name,
                    Model = x.Model,
                    NumberOfSeats = x.SeatsCount,
                    TransmissionType = x.HasAutomaticTransmission ? "Yes" : "No"
                }).FirstOrDefault();

            var message = new CarViewedMessage
            {
                CarId = car.Id,
                UserId = userId
            };
            await this.bus.Publish(message);
            return car;
        }
        public IEnumerable<CarInListModel> GetAll(int page)
        {
            return this.dbContext.Cars
                    .Skip((page - 1) * 9)
                    .Take(9)
                    .Select(x => new CarInListModel
                    {
                        Id = x.Id,
                        Name = x.CarName,
                        Category = x.Category.Name,
                        ImageUrl = x.ImageUrl,
                        IsAvailable = x.IsAvailable ? "Yes" : "No",
                        Model = x.Model,
                    }).ToList();
        }

        public IEnumerable<CarInListModel> GetCarsByDealerId(int dealerId)
        {
            return this.dbContext.Cars
                    .Where(x => x.DealerId == dealerId)
                    .OrderByDescending(x => x.Id)
                    .Select(x => new CarInListModel
                    {
                        Id = x.Id,
                        Name = x.CarName,
                        Category = x.Category.Name,
                        ImageUrl = x.ImageUrl,
                        IsAvailable = x.IsAvailable ? "Yes" : "No",
                        Model = x.Model,
                    }).ToList();
        }
        public bool CanDeleteOrEdit(int carId, int dealerId) 
        {
            return this.dbContext.Cars.Any(car => car.Id == carId && car.DealerId == dealerId);
        }
    }
}
