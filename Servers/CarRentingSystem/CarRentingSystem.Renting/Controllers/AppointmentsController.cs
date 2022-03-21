namespace CarRentingSystem.Renting.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using CarRentingSystem.Renting.Services;
    using Microsoft.AspNetCore.Authorization;
    using CarRentingSystem.Renting.ViewModels;
    using System.Threading.Tasks;
    [Route("/[controller]/[action]")]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService appointmentsService;

        public AppointmentsController(IAppointmentsService appointmentsService)
        {
            this.appointmentsService = appointmentsService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateAppointmentInputModel input)
        {
            await this.appointmentsService.CreateAsync(input);
            return this.Ok();
        }
    }
}
