using Microsoft.EntityFrameworkCore;
using ReservationService.Entities;

namespace ReservationService.Data;

public class ReservationServiceContext(DbContextOptions<ReservationServiceContext> options) : DbContext(options)
{
	public DbSet<PricePlan> PricePlans { get; set; }
	public DbSet<PricePlanItem> PricePlanItems { get; set; }
	public DbSet<Reservation> Reservations { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.OnModelCreatingBuilder();
	}
}
