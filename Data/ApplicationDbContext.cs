using Doctor_Appointment.Models;
using Doctor_Appointment.Clients;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Doctor_Appointment.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Appointment>()
                .HasKey(e => new { e.appointmentID});

            ////Change Tables name
            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UsersRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UsersLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");


            ////Ignor some Identity columns
            modelBuilder.Entity<IdentityUser>().ToTable("Users").Ignore(c => c.PhoneNumber);
            modelBuilder.Entity<IdentityUser>().ToTable("Users").Ignore(c => c.PhoneNumberConfirmed);
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<DailyAvailbility> dailyAvailbilities { get; set; }


    }
}