namespace CarRentingSystem.Renting.Services
{
    using CarRentingSystem.Renting.ViewModels;
    using System.Threading.Tasks;
    public interface IAppointmentsService
    {
        Task CreateAsync(CreateAppointmentInputModel input);
    }
}
