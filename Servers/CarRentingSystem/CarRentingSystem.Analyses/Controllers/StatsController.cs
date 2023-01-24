namespace CarRentingSystem.Analyses.Controllers
{
    using CarRentingSystem.Analyses.Models;
    using CarRentingSystem.Analyses.Services;
    using Microsoft.AspNetCore.Mvc;
    [Route("/[controller]/[action]")]
    public class StatsController : ControllerBase
    {
        private IStatisticsService statistics;

        public StatsController(IStatisticsService statistics)
            => this.statistics = statistics;
        [HttpGet]
        public ActionResult<StatisticsOutputModel> GetAll()
                => this.statistics.GetAll();
    }
}
