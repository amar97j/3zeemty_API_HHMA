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
    public class CatererstEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }

        public List<Services> serviceProvided { get; set; }
        
    }
    public class Services
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }    
        public CatererstEntity caterer { get; set; }
        public int catererID { get; set; }

    }

    public class Booking
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateOnly DateOnly { get; set; }

        public List<Services> serviceProvided { get; set; }


    }
    public class UserAEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    
        public bool IsAdmin { get; set; }

        private UserAEntity()
        {

        }
        public static UserAEntity Create(string username, string password, bool isAdmin)
        {
            return new UserAEntity
            {
                Username = username,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(password),
                IsAdmin = isAdmin,
            };
        }
    }
}
