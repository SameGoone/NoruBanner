using Microsoft.EntityFrameworkCore;
using NoruBanner.API.Data.Repositories;

namespace NoruBanner.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var frontendOrigins = "frontendOrigins";
			var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
			builder.Services.AddCors(options =>
			{
				options.AddPolicy(name: frontendOrigins, policy =>
				{
					if (allowedOrigins != null && allowedOrigins.Length > 0)
					{
						policy.WithOrigins(allowedOrigins)
							  .AllowAnyHeader()
							  .AllowAnyMethod();
					}
				});
			});

			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<AppDbContext>(options =>
				options.UseNpgsql(connectionString));

			builder.Services.AddScoped<IBannerEventRepository, BannerEventRepository>();

			builder.Services.AddControllers();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			if (!app.Environment.IsDevelopment())
			{
				app.UseHttpsRedirection();
			}

			app.UseCors(frontendOrigins);
			app.UseAuthorization();
			app.MapControllers();

			ApplyMigrations(app);

			app.Run();
		}

		public static void ApplyMigrations(IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
				try
				{
					Console.WriteLine("Applying database migrations...");
					dbContext.Database.Migrate();
					Console.WriteLine("Database migrations applied successfully.");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
				}
			}
		}
	}
}
