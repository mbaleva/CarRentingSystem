namespace CarRentingSystem.Cars.Controllers
{
    using CarRentingSystem.Cars.Models.Manufacturers;
    using CarRentingSystem.Cars.Services.Manufacturers;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [Route("/[controller]/[action]")]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerService manufacturerService;

        public ManufacturersController(IManufacturerService manufacturer)
        {
            this.manufacturerService = manufacturer;
        }
        public IEnumerable<ManufacturerModel> GetAll()
            => this.manufacturerService.GetAll();
    }
}
