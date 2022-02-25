namespace CarRentingSystem.Cars.Models.Cars
{
    using CarRentingSystem.Cars.Models.Dealers;
    public class CarByIdModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public string Category { get; set; }
        public bool IsAvailable { get; set; }

        public double PricePerDay { get; set; }
        public bool HasClimateControl { get; set; }

        public int NumberOfSeats { get; set; }

        public string TransmissionType { get; set; }
        public ById Dealer { get; set; }
    }
}
