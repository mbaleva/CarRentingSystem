namespace CarRentingSystem.Analyses.Controllers
{
    using CarRentingSystem.Analyses.Models;
    using CarRentingSystem.Analyses.Services;
    using Microsoft.AspNetCore.Mvc;
    public class StatsController : ControllerBase
    {
        private IStatisticsService statistics;

        public StatsController(IStatisticsService statistics)
        {
            this.statistics = statistics;
        }
        public ActionResult<StatisticsOutputModel> GetAll()
        {
            return this.statistics.GetAll();
        }
    }
}
