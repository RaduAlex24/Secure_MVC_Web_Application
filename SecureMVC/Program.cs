using HomeWork.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);


            string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(opts => {
                opts.UseSqlServer(connectionString);
            });
            builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();


            // Lockout
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Changing the password lenght
            builder.Services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
            });


            // CSRF attack protection
            builder.Services.ConfigureApplicationCookie(options => options.Cookie.SameSite = SameSiteMode.Strict);



            // Configuration
            var app = builder.Build();
            app.UseStaticFiles();
            app.MapDefaultControllerRoute();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            //app.MapGet("/", () => "Hello World!");


            // HTTPS redirect
            app.UseHttpsRedirection();
            app.UseCookiePolicy();

            // HSTS
            app.UseHsts();

            // Seeding data
            SeedData.EnsurePopulated(app);
            Task.Run(async () => {
                await SeedDataIdentity.EnsurePopulatedAsync(app);
            }).Wait();


            app.Run();
        }
    }
}