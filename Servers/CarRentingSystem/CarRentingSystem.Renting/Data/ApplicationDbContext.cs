namespace CarRentingSystem.Renting.Data
{
    using Microsoft.EntityFrameworkCore;
    using CarRentingSystem.Renting.Data.Models;
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() 
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {
        }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
