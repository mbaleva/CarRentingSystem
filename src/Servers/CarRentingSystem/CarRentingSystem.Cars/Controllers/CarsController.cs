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

    [ApiController]
    [Route("/[controller]/[action]")]
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
        [HttpGet]
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
        [HttpPost]
        public async Task<ActionResult<int>> Add([FromBody]AddCarInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest("Please submit correct data!");
            }
            return await this.cars.AddAsync(input, this.currentUser.UserId);
        }
        [HttpGet]
        public IEnumerable<CarInListModel> All([FromQuery]int page = 1)
            => this.cars.GetAll(page);
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CarByIdModel>> ById([FromQuery]int id, string name)
        {
            CarByIdModel model;

            if (this.User.Identity.IsAuthenticated)
            {
                model = await this.cars.GetCarById(id, currentUser.UserId);
            }
            else
            {
                model = await this.cars.GetCarById(id);
            }

            if (model == null)
            {
                return NotFound();
            }

            if (!string.Equals(name, model.Name, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid car name in the URL");
            }

            return model;
        }
        [Authorize]
        [HttpGet]
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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit([FromQuery] int dealerId,
            [FromQuery] int carId, [FromBody]AddCarInputModel input)
        {
            if (this.cars.CanDeleteOrEdit(carId, dealerId) || this.currentUser.IsAdmin)
            {
                await this.cars.UpdateCarAsync(input, carId);
                return this.Ok();
            }
            return this.BadRequest();
        }
        [HttpGet]
        public IEnumerable<CarInListModel> Search([FromQuery]SearchCarsInputModel input)
            => this.cars.SearchCars(input);   
    }
}
