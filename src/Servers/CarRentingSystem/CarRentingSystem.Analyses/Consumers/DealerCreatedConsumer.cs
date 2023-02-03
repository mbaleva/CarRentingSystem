namespace CarRentingSystem.Analyses.Consumers
{
    using CarRentingSystem.Analyses.Data;
    using CarRentingSystem.Common.Messages;
    using MassTransit;
    using System.Linq;
    using System.Threading.Tasks;
    using System;
    public class DealerCreatedConsumer : IConsumer<DealerCreatedMessage>
    {
        private readonly ApplicationDbContext dbContext;
        
        public DealerCreatedConsumer(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<DealerCreatedMessage> context)
        {
            Console.WriteLine("Dealer creating started...");
            var stats = this.dbContext.Data.Where(x => x.Id > 0).FirstOrDefault();
            stats.TotalDealers++;
            await this.dbContext.SaveChangesAsync();
            Console.WriteLine("Dealer created successfully :D");
        }
    }
}
