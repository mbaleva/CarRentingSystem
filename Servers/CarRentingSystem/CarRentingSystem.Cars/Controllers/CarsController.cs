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
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("[controller]/[action]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarsService cars;
        private readonly IDealersService dealers;
        private readonly ICurrentUserService currentUser;

        public CarsController(
            ICarsService cars,
            IDealersService dealers,
            ICategoriesService categories,
            IManufacturerService manufacturers,
            ICurrentUserService currentUser)
        {
            this.cars = cars;
            this.dealers = dealers;
            this.currentUser = currentUser;
        }
        public IEnumerable<CarInListModel> GetCarsByDealerId([FromQuery] int dealerId,
            [FromQuery]string userId)
        {
            if (this.dealers.CheckIfUserIsDealer(userId))
            {
                return this.cars.GetCarsByDealerId(dealerId);
            }
            return null;
        }
        [Authorize]
        public async Task<ActionResult<int>> Add([FromBody]AddCarInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest("Please submit correct data!");
            }
            return await this.cars.AddAsync(input, this.currentUser.UserId);
        }
        public IEnumerable<CarInListModel> All([FromQuery]int page = 1)
            => this.cars.GetAll(page);
        [Authorize]
        public async Task<ActionResult<CarByIdModel>> ById([FromQuery]int id)
        {
            CarByIdModel model;
            if (this.User.Identity.IsAuthenticated)
            {
                model = await this.cars.GetCarById(id, currentUser.UserId);
            }
            model = await this.cars.GetCarById(id);
            return model;
        }
        [Authorize]
        public async Task<IActionResult> Delete([FromQuery]int dealerId,
            [FromQuery]int carId)
        {
            if (this.cars.CanDeleteOrEdit(carId, dealerId) || this.currentUser.IsAdmin)
            {
                await this.cars.DeleteById(carId);
                return this.Ok();
            }
            return this.BadRequest();
        }
    }
}
