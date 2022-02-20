namespace CarRentingSystem.Cars.Data.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string ImageUrl { get; set; }
        public double PricePerDay { get; set; }

        public virtual CarOptions Options { get; set; }
        public bool IsAvailable { get; set; }
        public int DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
