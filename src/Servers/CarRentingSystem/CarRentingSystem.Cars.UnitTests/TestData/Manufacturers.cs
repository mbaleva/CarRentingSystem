using CarRentingSystem.Cars.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentingSystem.Cars.UnitTests.TestData
{
    public static class Manufacturers
    {
        public static List<Manufacturer> GetManufacturers() 
        {
            var result = new List<Manufacturer>();

            foreach (var item in Enumerable.Range(0, 50))
            {
                result.Add(new Manufacturer 
                {
                    Id = item,
                    Name = $"Test {item}",
                    Cars = new List<Car>()
                });
            }
            return result;
        }
    }
}
