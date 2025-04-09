using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<WashOrder> WashOrders { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Convert Enum Role to string
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>()
                .HasDefaultValue("User");

            // Define User - WashOrder (Customer)
            modelBuilder.Entity<WashOrder>()
                .HasOne(w => w.User)
                .WithMany(u => u.WashOrders)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define Washer - WashOrder (Washer)
            modelBuilder.Entity<WashOrder>()
                .HasOne(w => w.Washer)
                .WithMany()
                .HasForeignKey(w => w.WasherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define Car - WashOrder
            modelBuilder.Entity<WashOrder>()
                .HasOne(w => w.Car)
                .WithMany()
                .HasForeignKey(w => w.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define Car - CarImage Relationship (One-to-Many)
            modelBuilder.Entity<CarImage>()
                .HasOne(ci => ci.Car)
                .WithMany(c => c.CarImages)
                .HasForeignKey(ci => ci.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define Notification - User (One-to-Many)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
