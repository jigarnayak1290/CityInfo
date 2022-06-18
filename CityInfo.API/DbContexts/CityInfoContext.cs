using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;

        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                new City("New york")
                {
                    Id = 1,
                    Description = "The one with big park"
                },
                new City("Ahmedabad")
                {
                    Id = 2,
                    Description = "The one with SidiSaiyad-Jali"
                },
                new City("Rajkot")
                {
                    Id = 3,
                    Description = "The one which is my home town"
                }
                );

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                new PointOfInterest("Central park")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "Most Visited Central"
                },
                new PointOfInterest("Empire park")
                {
                    Id = 2,
                    CityId = 1,
                    Description = "Most Visited Empire"
                },
                new PointOfInterest("Sidi-Saiyad jali")
                {
                    Id = 3,
                    CityId = 2,
                    Description = "Most Visited Sidi-Saiyad"
                },
                new PointOfInterest("Corporate road")
                {
                    Id = 4,
                    CityId = 2,
                    Description = "Most Visited Corporate"
                },
                new PointOfInterest("Race course")
                {
                    Id = 5,
                    CityId = 3,
                    Description = "Most Visited Race course"
                },
                new PointOfInterest("Trikon baug")
                {
                    Id = 6,
                    CityId = 3,
                    Description = "Most Visited Trikon baug"
                }
                );
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("ConnectionString");
        //    base.OnConfiguring(optionsBuilder); 
        //}
    }
}
