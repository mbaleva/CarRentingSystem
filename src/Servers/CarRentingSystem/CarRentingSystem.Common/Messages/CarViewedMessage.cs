namespace CarRentingSystem.Common.Messages
{
    public class CarViewedMessage
    {
        public int CarId { get; set; }
        public string ManufacturerName { get; set; }
        public string Model { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
