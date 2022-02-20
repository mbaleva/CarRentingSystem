namespace CarRentingSystem.Analyses.Consumers
{
    using CarRentingSystem.Analyses.Data;
    using CarRentingSystem.Common.Messages;
    using MassTransit;
    using System.Linq;
    using System.Threading.Tasks;

    public class CarCreatedConsumer : IConsumer<CarCreatedMessage>
    {
        private readonly ApplicationDbContext dbContext;
        public async Task Consume(ConsumeContext<CarCreatedMessage> context)
        {
            var stats = this.dbContext.Data.Where(x => x.Id > 0).FirstOrDefault();
            stats.TotalCars++;
            await this.dbContext.SaveChangesAsync();
        }
    }
}
