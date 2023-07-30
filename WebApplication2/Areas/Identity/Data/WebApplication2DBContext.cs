using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Areas.Identity.Data;
using WebApplication2.Data.Model;

namespace WebApplication2.Areas.Identity.Data;

public class WebApplication2DBContext : IdentityDbContext<WebApplication2Admin>
{
    public DbSet<Escort> Escorts { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomImage> RoomImages { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }

    public WebApplication2DBContext(DbContextOptions<WebApplication2DBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Escort>(entity =>
        {
            entity.HasKey(e => e.EscortId);
            entity.Property(e => e.ReservationId).ValueGeneratedOnAdd();
            entity.Property(entity => entity.Email).IsRequired();
            entity.HasOne(e => e.Reservation).WithMany(e => e.Escorts).HasForeignKey(e => e.ReservationId);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId);
            entity.Property(e => e.ReservationId).ValueGeneratedOnAdd();
            entity.Property(e => e.PhoneNumber).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.CheckIn).IsRequired();
            entity.Property(e => e.CheckOut).IsRequired();
            entity.Property(e => e.Price).IsRequired();
            entity.HasOne(e => e.Room).WithMany(e => e.Reservations).HasForeignKey(e => e.RoomId);
        });
        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId);
            entity.Property(e => e.RoomId).ValueGeneratedOnAdd();
            entity.Property(e => e.AdultPrice).IsRequired();
            entity.Property(e => e.ChildrenPrice).IsRequired();
            entity.Property(e => e.RoomNumber).IsRequired();
            entity.HasOne(e => e.RoomType).WithMany(e => e.Rooms).HasForeignKey(e => e.RoomTypeId);
        });

        modelBuilder.Entity<RoomImage>(entity =>
        {
            entity.HasKey(entity => entity.RoomImageId);
            entity.Property(e => e.RoomImageId).ValueGeneratedOnAdd();
            entity.Property(e => e.ImageUrl).IsRequired().HasColumnType("varchar(max)");
            entity.HasOne(e => e.Room).WithMany(e => e.RoomImages).HasForeignKey(e => e.RoomId);
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.HasKey(e => e.RoomTypeId);
            entity.Property(e => e.RoomTypeId).ValueGeneratedOnAdd();
            entity.Property(e => e.Type).IsRequired();
            /* entity.Property(e => e.Price).IsRequired();*/
        });
        base.OnModelCreating(modelBuilder);
    }
}
