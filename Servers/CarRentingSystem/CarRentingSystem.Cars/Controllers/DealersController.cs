namespace CarRentingSystem.Cars.Controllers
{
    using CarRentingSystem.Cars.Models.Dealers;
    using CarRentingSystem.Cars.Services.Dealers;
    using CarRentingSystem.Common.Services.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
        public bool IsDealer([FromQuery]string userId)
            => this.dealersService.CheckIfUserIsDealer(userId);
        [Authorize]
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
        public ActionResult<ById> GetDealerById([FromQuery]int dealerId,
            [FromQuery]string userId)
        {
            if (this.dealersService.CheckIfUserIsDealer(userId))
            {
                return this.dealersService.GetDealerById(dealerId);
            }
            return this.BadRequest("You are not a dealer!!!");
        }
        public IEnumerable<DealerInListModel> All(int id)
        {
            return this.dealersService.GetAllDealers(id, ITEMS_PER_PAGE);
        }
        [HttpPost]
        public async Task<ActionResult> Edit([FromBody]EditDealerInputModel model)
        {
            if (this.userService.IsAdmin || this.dealersService.CanEdit(this.userService.UserId, model.Id))
            {
                await this.dealersService.UpdateDealer(model);
            }
            return this.Ok();
        }
    }
}
