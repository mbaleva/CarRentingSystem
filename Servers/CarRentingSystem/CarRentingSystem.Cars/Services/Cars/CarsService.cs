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
    using Microsoft.EntityFrameworkCore;

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

        public async Task UpdateCarAsync(AddCarInputModel model, int id)
        {
            var car = this.dbContext.Cars.Where(x => x.Id == id)
                .Include(x => x.Manufacturer)
                .FirstOrDefault();

            car.CarName = model.Name;
            car.CategoryId = model.CategoryId;
            car.HasAutomaticTransmission = model.HasAutomaticTransmission;
            car.HasClimateControl = model.HasClimateControl;
            car.ImageUrl = model.ImageUrl;
            car.Model = car.Model;
            car.PricePerDay = car.PricePerDay;
            car.SeatsCount = car.SeatsCount;

            if (car.Manufacturer.Name != model.ManufacturerName)
            {
                car.Manufacturer = await this.HandleManufacturer(model.ManufacturerName);
            }
            await this.dbContext.SaveChangesAsync();
        }
        private async Task<Manufacturer> HandleManufacturer(string name) 
        {
            Manufacturer manufacturer;
            if (!this.dbContext.Manufacturers.Any(x => x.Name == name))
            {
                await this.dbContext.Manufacturers.AddAsync(new Manufacturer { Name = name });
            }
            manufacturer = this.dbContext.Manufacturers.Where(x => x.Name == name).FirstOrDefault();
            await this.dbContext.SaveChangesAsync();
            return manufacturer;
        }
        public IEnumerable<CarInListModel> SearchCars(SearchCarsInputModel model)
        {
            IEnumerable<CarInListModel> cars = new List<CarInListModel>();

            if (model.CategoryId == -1 && model.ManufacturerId == -1 && model.SearchTerm != null)
            {
                cars = this.SearchCarsByName(model.SearchTerm);
            }
            if (model.SearchTerm != null && model.CategoryId != -1)
            {
                cars = this.SearchCarsByNameAndCategory(model.CategoryId, model.SearchTerm);
            }
            if (model.SearchTerm != null && model.CategoryId != -1 && model.ManufacturerId != -1)
            {
                cars = this.SearchCarsByNameCategoryAndManufacturer(model.CategoryId, model.ManufacturerId, model.SearchTerm);
            }
            return cars;
        }

        private IEnumerable<CarInListModel> SearchCarsByNameCategoryAndManufacturer(
            int categoryId,
            int manufacturerId,
            string searchTerm)
            => this.dbContext.Cars.Where(x => x.CategoryId == categoryId &&
                x.ManufacturerId == manufacturerId &&
                x.CarName.Contains(searchTerm))
                .Select(x => new CarInListModel
                {
                    Id = x.Id,
                    Category = x.Category.Name,
                    ImageUrl = x.ImageUrl,
                    IsAvailable = x.IsAvailable.ToString(),
                    Model = x.Model,
                    Name = x.CarName,
                }).ToList();

        private IEnumerable<CarInListModel> SearchCarsByNameAndCategory(
            int categoryId,
            string searchTerm) => 
                this.dbContext.Cars.Where(x => x.CategoryId == categoryId &&
                x.CarName.Contains(searchTerm))
                .Select(x => new CarInListModel 
                {
                    Id = x.Id,
                    Category = x.Category.Name,
                    ImageUrl = x.ImageUrl,
                    IsAvailable = x.IsAvailable.ToString(),
                    Model = x.Model,
                    Name = x.CarName,
                }).ToList();
                

        private IEnumerable<CarInListModel> SearchCarsByName(
            string searchTerm)
            =>  this.dbContext.Cars
                    .Where(x => x.CarName.Contains(searchTerm))
                    .Select(x => new CarInListModel
                    {
                        Name = x.CarName,
                        Model = x.Model,
                        IsAvailable = x.IsAvailable.ToString(),
                        Id = x.Id,
                        ImageUrl = x.ImageUrl,
                        Category = x.Category.Name,
                    }).ToList();
    }
}
