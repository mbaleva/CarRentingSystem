﻿namespace CarRentingSystem.Identity.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using CarRentingSystem.Identity.Services;
    using CarRentingSystem.Identity.ViewModels.Users;
    using CarRentingSystem.Identity.Services.Users;

    [ApiController]
    [Route("[controller]/[action]")]
    public class IdentityController : ControllerBase
    {
        private readonly IUsersService usersService;

        public IdentityController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody]RegisterInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }
            await this.usersService.Register(model);
            return this.Ok();
        }
        [HttpPost]
        public async Task<ActionResult<LoginSuccesModel>> Login([FromBody]LoginInputModel model)
        {
            var data = await this.usersService.Login(model);
            
            if (data == null)
            {
                return this.BadRequest();
            }
            return data;
        }
    }
}
