namespace CarRentingSystem.Cars.Services.Dealers
{
    using CarRentingSystem.Cars.Data.Models;
    using CarRentingSystem.Cars.Models.Dealers;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IDealersService
    {
        Task<int> AddDealerAsync(AddDealerInputModel model);
        bool IsDealer(string userId);
        int GetDealerIdByUser(string userId);
        ById GetDealerById(int id);
        Dealer GetDealerByUserId(string userId);
        IEnumerable<DealerInListModel> GetAllDealers(int page, int itemsPerPage);
        bool CanEdit(string userId, int dealerId);
        Task UpdateDealer(EditDealerInputModel model);
    }
}
