using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentingSystem.Cars.UnitTests.Helpers
{
    using CarRentingSystem.Cars.Data;
    using Microsoft.EntityFrameworkCore;
    public static class DbContextHelper
    {
        public static ApplicationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("cars-test-db")
                    .Options;
            return new ApplicationDbContext(options);
        }
    }
}
