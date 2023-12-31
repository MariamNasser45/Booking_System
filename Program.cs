using Doctor_Appointment.Data;
using Doctor_Appointment.Models;
using Doctor_Appointment.Repo;
using Doctor_Appointment.Repo.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using static System.Formats.Asn1.AsnWriter;
using PayPal.Api;
using Doctor_Appointment.Clients;

namespace Doctor_Appointment
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
            //.AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddRoles<IdentityRole>()
     .AddEntityFrameworkStores<ApplicationDbContext>()
     .AddDefaultUI()
     .AddDefaultTokenProviders();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddAuthentication().AddGoogle(options =>
            {
                IConfigurationSection googleAuthSection = builder.Configuration.GetSection("Authentication:Google");
                options.ClientId = googleAuthSection["ClientId"];
                options.ClientSecret = googleAuthSection["ClientSecret"];
            })

             .AddFacebook(options =>
             {
                 IConfigurationSection facebookAuthSection = builder.Configuration.GetSection("Authentication:Facebook");
                 options.ClientId = facebookAuthSection["ClientId"];
                 options.ClientSecret = facebookAuthSection["ClientSecret"];
             });

            builder.Services.AddSingleton(x => new PaypalClient(
            builder.Configuration["Paypal:ClientId"],
            builder.Configuration["Paypal:ClientSecret"],
            builder.Configuration["Paypal:Mode"]
             )
        );
            //our own services

            builder.Services.AddScoped<IDoctorRepo, DoctorRepoServices>();
            builder.Services.AddScoped<IPatientRepo,PatientRepoServices>();
            builder.Services.AddScoped<IAppointmentRepo, AppointementRepoServices>();
            builder.Services.AddScoped<IDailyAvailbilityRepo, DailyAvailbiltyRepoServices>();


            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
          
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();


            app.Run();
        }
    }
}