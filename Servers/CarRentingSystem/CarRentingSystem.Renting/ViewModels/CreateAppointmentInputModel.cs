namespace CarRentingSystem.Renting.ViewModels
{
    using System;
    public class CreateAppointmentInputModel
    {
        public int CarId { get; set; }
        public string? UserId { get; set; }

        public double TotalPrice { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
