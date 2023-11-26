using Microsoft.EntityFrameworkCore;

namespace CLH_Final_Project.Entities.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Packages> Packages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PaymentReference> Payments { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }


    }




    
}
