namespace CarRentingSystem.Cars.Models.Cars
{
    public class SearchCarsInputModel
    {
        public string SearchTerm { get; set; }
        public int ManufacturerId { get; set; }
        public int CategoryId { get; set; }
    }
}
