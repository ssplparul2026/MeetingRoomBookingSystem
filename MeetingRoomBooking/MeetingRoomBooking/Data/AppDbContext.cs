using MeetingRoomBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoomBooking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Booking>()
                .Property(x => x.Status)
                .HasConversion<string>();
        }
    }
}
