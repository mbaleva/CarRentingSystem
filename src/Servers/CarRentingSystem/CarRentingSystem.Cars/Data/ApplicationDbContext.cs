using CarRentingSystem.Cars.Data.Models;
using CarRentingSystem.Common.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CarRentingSystem.Cars.Data
{
    public class ApplicationDbContext : MessagesDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Dealer> Dealers { get; set; }

        protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();
    }
}
