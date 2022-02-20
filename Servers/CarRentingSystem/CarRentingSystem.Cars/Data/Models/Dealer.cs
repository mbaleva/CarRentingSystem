namespace CarRentingSystem.Cars.Data.Models
{
    using System.Collections.Generic;
    public class Dealer
    {
        public Dealer()
        {
            this.Cars = new HashSet<Car>();
        }
        public int Id { get; set; }
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
