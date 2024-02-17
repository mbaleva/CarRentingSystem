using CarRentingSystem.Common.Data.EntityConfigurations;
using CarRentingSystem.Common.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CarRentingSystem.Common.Data;

public abstract class MessagesDbContext : DbContext
{
	protected MessagesDbContext(DbContextOptions options)
		:base(options)
	{
	}

	public DbSet<Message> Messages { get; set; }

	protected abstract Assembly ConfigurationsAssembly { get; }

	protected override void OnModelCreating(ModelBuilder builder) 
	{
		builder.ApplyConfiguration(new MessageEntityConfiguration());
		builder.ApplyConfigurationsFromAssembly(ConfigurationsAssembly);

		base.OnModelCreating(builder);
	}
}
