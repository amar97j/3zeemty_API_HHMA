using Microsoft.EntityFrameworkCore;

namespace ProductApi.Models.Entites
{
    public class CaterContext : DbContext
    {
        public CaterContext(DbContextOptions<CaterContext> options) : base(options)
        {

        }
        public DbSet<CatererstEntity> Cateres { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<UserAEntity> UserAccounts { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatererstEntity>().HasData(new CatererstEntity
            {
                Id = 1,
                Name = "COCOVIA",
                Type = "COFFEE", 
                Description = "COFFEE CORNER",
             
            }
            );

            modelBuilder.Entity<Services>().HasData(new Services
            {
                Id = 1,
                Name = "Mohammad",
                Price = 2.0000M,
                catererID = 1

            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
