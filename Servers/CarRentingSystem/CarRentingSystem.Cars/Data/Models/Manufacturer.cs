namespace CarRentingSystem.Cars.Data.Models
{
    using System.Collections.Generic;
    public class Manufacturer
    {
        public Manufacturer()
        {
            this.Cars = new HashSet<Car>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
