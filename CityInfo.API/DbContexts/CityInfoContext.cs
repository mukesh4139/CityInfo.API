﻿using CityInfo.API.Entities;
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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("New York City")
                {
                    Id = 1,
                    Description = "The one with that big park."
                },
                new City("Antwerp")
                {
                    Id = 2,
                    Description = "The one with that big park."
                },
                new City("Paris")
                {
                    Id = 3,
                    Description = "The one with that big park."
                });

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                    new PointOfInterest("Central Park")
                    {
                        Id = 1,
                        CityId = 1,
                        Description = "The most visited urban park"
                    },
                    new PointOfInterest("Empire State Building")
                    {
                        Id = 2,
                        CityId = 1,
                        Description = "A 102-story skyscraper located in Midtown Manhattan"
                    },
                    new PointOfInterest("Cathedral")
                    {
                        Id = 3,
                        CityId = 2,
                        Description = "The Gothic style cathedral"
                    },
                    new PointOfInterest("Antwerp Central Station")
                    {
                        Id = 4,
                        CityId = 2,
                        Description = "The finest example of railway architecture in Belgium"
                    },
                    new PointOfInterest("Eiffel Tower")
                    {
                        Id = 5,
                        CityId = 3,
                        Description = "The wrought iron lattice tower"
                    },
                    new PointOfInterest("The Louvre")
                    {
                        Id = 6,
                        CityId = 3,
                        Description = "The world's largest museum"
                    }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
