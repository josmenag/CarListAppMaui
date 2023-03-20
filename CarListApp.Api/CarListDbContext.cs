using System;
using Microsoft.EntityFrameworkCore;

namespace CarListApp.Api
{
	public class CarListDbContext : DbContext
	{
		public CarListDbContext(DbContextOptions<CarListDbContext> options): base(options)
		{

		}

		public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Car>().HasData(
				new Car
				{
					Id = 1,
					Make = "Nissan",
					Model = "Atlas",
					Vin = "ABC1"
				},
                new Car
                {
                    Id = 2,
                    Make = "Honda",
                    Model = "Stream",
                    Vin = "ABC2"
                },
                new Car
                {
                    Id = 3,
                    Make = "Honda",
                    Model = "Civid",
                    Vin = "ABC3"
                },
                new Car
                {
                    Id = 4,
                    Make = "Honda",
                    Model = "Fit",
                    Vin = "ABC4"
                },
                new Car
                {
                    Id = 5,
                    Make = "Nissan",
                    Model = "Murano",
                    Vin = "ABC5"
                },
                new Car
                {
                    Id = 6,
                    Make = "Nissan",
                    Model = "Dualis",
                    Vin = "ABC6"
                },
                new Car
                {
                    Id = 7,
                    Make = "Audi",
                    Model = "A5",
                    Vin = "ABC7"
                },
                new Car
                {
                    Id = 8,
                    Make = "Jaguar",
                    Model = "F-Pace",
                    Vin = "ABC8"
                }

            );
        }
    }
}