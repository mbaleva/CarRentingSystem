namespace CarRentingSystem.Cars.Services.Cars
{
    using CarRentingSystem.Cars.Models.Cars;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICarsService
    {
        IEnumerable<CarInListModel> GetAll(int page);
        Task<CarByIdModel> GetCarById(int id, string userId = null);
        Task<bool> DeleteById(int id);
        Task<int> AddAsync(AddCarInputModel model, string userId);
    }
}
