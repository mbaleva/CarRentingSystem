namespace CarRentingSystem.Cars.Controllers
{
    using CarRentingSystem.Cars.Models.Cars;
    using CarRentingSystem.Cars.Services.Cars;
    using CarRentingSystem.Cars.Services.Categories;
    using CarRentingSystem.Cars.Services.Dealers;
    using CarRentingSystem.Cars.Services.Manufacturers;
    using CarRentingSystem.Common.Services.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CarsController : ControllerBase
    {
        private readonly ICarsService cars;
        private readonly ICurrentUserService currentUser;

        public CarsController(
            ICarsService cars,
            IDealersService dealers,
            ICategoriesService categories,
            IManufacturerService manufacturers,
            ICurrentUserService currentUser)
        {
            this.cars = cars;
            this.currentUser = currentUser;
        }
        [Authorize]
        public async Task<ActionResult<int>> Add([FromBody] AddCarInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest("Please submit correct data!");
            }
            return await this.cars.AddAsync(model, this.currentUser.UserId);
        }
        public IEnumerable<CarInListModel> All(int id)
            => this.cars.GetAll(id);

        public async Task<ActionResult<CarByIdModel>> ById(int id)
        {
            CarByIdModel model;
            if (this.User.Identity.IsAuthenticated)
            {
                model = await this.cars.GetCarById(id, currentUser.UserId);
            }
            model = await this.cars.GetCarById(id);
            return model;
        }

    }
}
