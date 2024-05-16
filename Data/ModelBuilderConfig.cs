using Microsoft.EntityFrameworkCore;
using ReservationService.Entities;

namespace ReservationService.Data;

public static class ModelBuilderConfig
{
	public static void OnModelCreatingBuilder(this ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<PricePlanItem>(d =>
		{
			d.HasOne(ppi => ppi.PricePlan)
			  .WithMany(pp => pp.Items)
			  .HasForeignKey(ppi => ppi.PricePlanId);

			d.Navigation(c => c.PricePlan).AutoInclude();
		});

		modelBuilder.Entity<Reservation>(c =>
		{
			c.HasOne(r => r.PricePlanItem)
			  .WithMany(ppi => ppi.Reservations)
			  .HasForeignKey(r => r.PricePlanItemId);
		});
	}
}
