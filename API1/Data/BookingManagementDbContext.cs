using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementApp.Data
{
    public class BookingManagementDbContext : DbContext
    {
        public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base (options) {  }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room>  Rooms { get; set; }
        public DbSet<University> Universities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasIndex(e => new
            {
                e.NIK,
                e.Email,
                e.PhoneNumber
            }).IsUnique();

            modelBuilder.Entity<University>()
                .HasMany(e => e.Educations)
                .WithOne(u => u.University)
                .HasForeignKey(e => e.UniversityGuid);

            modelBuilder.Entity<Education>()
                .HasOne(e => e.Employee)
                .WithOne(ed => ed.Education)
                .HasForeignKey<Education>(ed => ed.Guid);

            modelBuilder.Entity<Employee>()
                .HasMany(b => b.Bookings)
                .WithOne(e => e.Employee)
                .HasForeignKey(b => b.EmployeeGuid);

            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(e => e.Employee)
                .HasForeignKey<Account>(a => a.Guid);

            modelBuilder.Entity<Account>()
                .HasMany(ar => ar.AccountRoles)
                .WithOne(a => a.Account)
                .HasForeignKey(ar => ar.AccountGuid);

            modelBuilder.Entity<AccountRole>()
                .HasOne(r => r.Role)
                .WithMany(ar => ar.AccountRoles)
                .HasForeignKey(ar => ar.RoleGuid);

            modelBuilder.Entity<Room>()
                .HasMany(b => b.Bookings)
                .WithOne(r => r.Room)
                .HasForeignKey(b => b.RoomGuid);
        }

        

    }
}
