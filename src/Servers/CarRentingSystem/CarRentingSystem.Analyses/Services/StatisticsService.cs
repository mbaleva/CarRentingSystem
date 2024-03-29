﻿namespace CarRentingSystem.Analyses.Services
{
    using CarRentingSystem.Analyses.Data;
    using CarRentingSystem.Analyses.Models;
    using System.Linq;

    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext dbContext;
        public StatisticsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public StatisticsOutputModel GetAll()
        {
            var stats = this.dbContext.Data.Where(x => x.Id > 0).FirstOrDefault();

            if (stats is null)
            {
                return new StatisticsOutputModel();
            }

            var model = new StatisticsOutputModel
            {
                TotalCars = stats.TotalCars,
                TotalDealers = stats.TotalDealers,
                TotalRentedCars = stats.TotalRentedCars,
            };
            return model;
        }
    }
}
