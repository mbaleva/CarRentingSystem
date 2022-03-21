namespace CarRentingSystem.Renting.Data.Models
{
    using System;
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalPrice { get; set; }
        public int CarId { get; set; }
        public string UserId { get; set; }
    }
}
