using Microsoft.EntityFrameworkCore;
using project.Data.Entities;

namespace project.Data
{
    public class TrialContext : DbContext
    {
        public TrialContext(DbContextOptions<TrialContext> options)
            : base(options) { }

        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>(entry =>
            {
                entry.HasKey(e => e.Id);

                entry.Property(e => e.Number)
                    .IsRequired(true);

                entry.Property(e => e.Floor)
                    .IsRequired(true);

                entry.Property(e => e.Type)
                    .IsRequired(true);

                entry.HasMany(r => r.Guests)
                    .WithOne(g => g.Room)
                    .HasForeignKey(g => g.RoomId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Guest>(entry =>
            {
                entry.HasKey(e => e.Id);

                entry.Property(e => e.FirstName)
                    .HasMaxLength(200)
                    .IsRequired(true);

                entry.Property(e => e.LastName)
                    .HasMaxLength(400)
                    .IsRequired(true);

                entry.Property(e => e.DOB)
                    .IsRequired(true);

                entry.Property(e => e.Address)
                    .IsRequired(true)
                    .HasMaxLength(600);

                entry.Property(e => e.Nationality)
                    .IsRequired(true);

                entry.Property(e => e.CheckInDate)
                    .IsRequired(true);

                entry.Property(e => e.CheckOutDate)
                    .IsRequired(true);

                entry.HasOne(g => g.Room)
                    .WithMany(r => r.Guests)
                    .HasForeignKey(g => g.RoomId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}