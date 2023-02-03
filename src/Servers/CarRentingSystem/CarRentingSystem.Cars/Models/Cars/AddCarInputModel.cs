namespace CarRentingSystem.Cars.Models.Cars
{
    using System.ComponentModel.DataAnnotations;
    public class AddCarInputModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string ManufacturerName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public double PricePerDay { get; set; }
        public bool HasClimateControl { get; set; }
        [Required]
        public bool HasAutomaticTransmission { get; set; }
        public int SeatsCount { get; set; }
    }
}
