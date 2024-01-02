namespace CarRentingSystem.Analyses.Consumers
{
    using CarRentingSystem.Analyses.Data;
    using CarRentingSystem.Common.Messages;
    using MassTransit;
    using System.Linq;
    using System.Threading.Tasks;
    using System;

    public class CarCreatedConsumer : IConsumer<CarCreatedMessage>
    {
        private readonly ApplicationDbContext dbContext;

        public CarCreatedConsumer(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<CarCreatedMessage> context)
        {
            Console.WriteLine("Car creating started...");
            var stats = this.dbContext.Data.Where(x => x.Id > 0).FirstOrDefault();

            if (stats is null)
            {
                stats = new Data.Models.Data();
                this.dbContext.Data.Add(stats);
            }
            stats.TotalCars++;
            await this.dbContext.SaveChangesAsync();
        }
    }
}
