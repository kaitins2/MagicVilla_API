using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Occupancy = 4,
                    Sqft = 100,
                    description = "Has pool and is nice",
                    rate = 100,
                    imageUrl = "https://www.google.com",
                    amenities = "Pool",
                    CreatedDate = System.DateTime.Now,
                    UpdatedDate = System.DateTime.Now
                }
                );

        }
    }
}
