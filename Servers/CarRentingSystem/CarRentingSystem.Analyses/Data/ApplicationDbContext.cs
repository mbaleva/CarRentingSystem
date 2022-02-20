namespace CarRentingSystem.Analyses.Data
{
    using Microsoft.EntityFrameworkCore;
    using CarRentingSystem.Analyses.Data.Models;
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Data> Data { get; set; }
        public DbSet<CarView> CarsViews { get; set; }
        public DbSet<CarLiked> CarsLikes { get; set; }
    }
}
