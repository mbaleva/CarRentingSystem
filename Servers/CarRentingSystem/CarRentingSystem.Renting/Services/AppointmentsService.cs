namespace CarRentingSystem.Renting.Services
{
    using CarRentingSystem.Renting.Data;
    using CarRentingSystem.Renting.ViewModels;
    using CarRentingSystem.Renting.Data.Models;

    public class AppointmentsService : IAppointmentsService
    {
        private readonly ApplicationDbContext dbContext;

        public AppointmentsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task CreateAsync(CreateAppointmentInputModel input)
        {
            var appointment = new Appointment 
            {
                CarId = input.CarId,
                UserId = input.UserId,
                TotalPrice = input.TotalPrice,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
            };
            await this.dbContext.Appointments.AddAsync(appointment);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
