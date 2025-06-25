using Microsoft.EntityFrameworkCore;
using NoruBanner.API.Common;
using NoruBanner.API.Data;
using NoruBanner.API.Models;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<BannerEvent> BannerEvents { get; set; }
	public DbSet<EventType> EventTypes { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		foreach (var entityType in modelBuilder.Model.GetEntityTypes())
		{
			foreach (var property in entityType.GetProperties())
			{
				if (property.ClrType == typeof(DateTime))
				{
					property.SetValueConverter(new DateTimeUtcConverter());
				}
			}

			var createdAtProperty = entityType.FindProperty("CreatedAt");
			if (createdAtProperty != null && createdAtProperty.ClrType == typeof(DateTime))
			{
				createdAtProperty.SetDefaultValueSql("now() at time zone 'utc'");
			}
		}

		modelBuilder.Entity<EventType>().HasData(
			new EventType { Id = Constants.EventTypes.View, Name = "View" },
			new EventType { Id = Constants.EventTypes.Click, Name = "Click" }
		);
	}
}