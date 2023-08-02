using Microsoft.EntityFrameworkCore;
using RemoteControllerApi.Domain.Entities;

namespace RemoteControllerApi.Infrastructure;

public sealed class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions options)
		: base(options)
	{
	}

    public DbSet<Room> Rooms { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
