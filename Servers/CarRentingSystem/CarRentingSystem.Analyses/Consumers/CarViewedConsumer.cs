namespace CarRentingSystem.Analyses.Consumers
{
    using CarRentingSystem.Analyses.Data;
    using CarRentingSystem.Analyses.Data.Models;
    using CarRentingSystem.Common.Messages;
    using MassTransit;
    using System.Threading.Tasks;

    public class CarViewedConsumer : IConsumer<CarViewedMessage>
    {
        private readonly ApplicationDbContext dbContext;
        public CarViewedConsumer(ApplicationDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task Consume(ConsumeContext<CarViewedMessage> context)
        {
            var message = context.Message;
            var carView = new CarView 
            {
                CarId = message.CarId,
                UserId = message.UserId,
            };
            await this.dbContext.CarsViews.AddAsync(carView);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
