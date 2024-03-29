﻿namespace CarRentingSystem.Cars.Controllers
{
    using CarRentingSystem.Cars.Models.Dealers;
    using CarRentingSystem.Cars.Services.Dealers;
    using CarRentingSystem.Common.Filters;
    using CarRentingSystem.Common.Services.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DealersController : ControllerBase
    {
        private readonly IDealersService dealersService;
        private readonly ICurrentUserService userService;
        private const int ITEMS_PER_PAGE = 9;

        public DealersController(
            IDealersService dealersService, 
            ICurrentUserService userService)
        {
            this.dealersService = dealersService;
            this.userService = userService;
        }
        [HttpGet]
        public bool IsDealer([FromQuery]string userId)
            => this.dealersService.CheckIfUserIsDealer(userId);
        [Authorize]
        [HttpGet]
        public ActionResult<int> GetDealerId()
        {
            if (!this.dealersService.IsDealer(this.userService.UserId))
            {
                this.Unauthorized();
            }
            return this.dealersService.GetDealerIdByUser(this.userService.UserId);
        }
        [HttpPost]
        public async Task<ActionResult<int>> Add([FromBody]AddDealerInputModel model)
        {
            int id = await this.dealersService.AddDealerAsync(model);
            return id;
        }
        [HttpGet]
        public ActionResult<ById> GetDealerById([FromQuery]int dealerId,
            [FromQuery]string userId)
        {
            if (this.dealersService.CheckIfUserIsDealer(userId))
            {
                return this.dealersService.GetDealerById(dealerId);
            }
            return this.BadRequest("You are not a dealer!!!");
        }
        [HttpGet]
        [ServiceFilter(typeof(CacheActionAttribute))]
        public IEnumerable<DealerInListModel> All(int id)
        {
            return this.dealersService.GetAllDealers(id, ITEMS_PER_PAGE);
        }
        [HttpPost]
        public async Task<ActionResult> Edit([FromBody]EditDealerInputModel model, [FromQuery]int id)
        {
            if (this.userService.IsAdmin || this.dealersService.CanEdit(this.userService.UserId, id))
            {
                await this.dealersService.UpdateDealer(model);
            }
            return this.Ok();
        }
    }
}
