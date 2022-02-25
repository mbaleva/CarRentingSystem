namespace CarRentingSystem.Cars.Services.Dealers
{
    using CarRentingSystem.Cars.Data;
    using CarRentingSystem.Cars.Data.Models;
    using CarRentingSystem.Cars.Models.Dealers;
    using CarRentingSystem.Common.Services.Users;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using MassTransit;
    using System.Threading.Tasks;
    using CarRentingSystem.Common.Messages;

    public class DealersService : IDealersService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICurrentUserService userService;
        private readonly IBus bus;

        public DealersService(
            ApplicationDbContext dbContext,
            ICurrentUserService userService,
            IBus bus)
        {
            this.dbContext = dbContext;
            this.userService = userService;
            this.bus = bus;
        }
        public bool CheckIfUserIsDealer(string userId)
            => this.dbContext.Dealers.Any(x => x.UserId == userId);
        public int GetDealerIdByUser(string userId)
        {
            return this.dbContext.Dealers.Where(x => x.UserId == userId)
                .FirstOrDefault().Id;
        }
        public bool IsDealer(string userId)
        {
            return this.dbContext.Dealers.Any(x => x.UserId == userId);
        }
        public Dealer GetDealerByUserId(string userId)
        {
            return this.dbContext.Dealers.Where(x => x.UserId == userId).FirstOrDefault();
        }

        public async Task<int> AddDealerAsync(AddDealerInputModel model)
        {
            Dealer dealer = new Dealer
            {
                UserId = this.userService.UserId,
                PhoneNumber = model.PhoneNumber,
                FullName = model.Name,
            };
            await this.dbContext.Dealers.AddAsync(dealer);
            await this.dbContext.SaveChangesAsync();

            var message = new DealerCreatedMessage();
            await this.bus.Publish(message);
            return dealer.Id;
        }
        public ById GetDealerById(int id)
        {
            var dealerAsQueryable = this.dbContext.Dealers.Where(x => x.Id == id);
            var dealer = dealerAsQueryable.Select(x => new ById
            {
                Id = x.Id,
                Name = x.FullName,
                PhoneNumber = x.PhoneNumber,
                TotalCars = x.Cars.Count,
            })
               .FirstOrDefault();
            return dealer;
        }
        public IEnumerable<DealerInListModel> GetAllDealers(int page, int itemsPerPage)
        {
            var dealers = this.dbContext.Dealers
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new DealerInListModel { Name = x.FullName, TotalCars = x.Cars.Count })
                .ToList();

            return dealers;
        }

        public bool CanEdit(string userId, int dealerId)
        {
            return this.dbContext.Dealers.Any(x => x.UserId == userId && x.Id == dealerId);
        }
        public async Task UpdateDealer(EditDealerInputModel model)
        {
            var dealer = this.dbContext.Dealers.Where(x => x.Id == model.Id).FirstOrDefault();
            dealer.FullName = model.FullName;
            dealer.PhoneNumber = model.PhoneNumber;
            await this.dbContext.SaveChangesAsync();
        }
    }
}
