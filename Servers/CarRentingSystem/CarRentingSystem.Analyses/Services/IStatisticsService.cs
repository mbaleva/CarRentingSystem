namespace CarRentingSystem.Analyses.Services
{
    using CarRentingSystem.Analyses.Models;
    public interface IStatisticsService
    {
        StatisticsOutputModel GetAll();
    }
}
