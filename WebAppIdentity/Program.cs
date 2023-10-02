using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebAppIdentity.Data;

namespace WebAppIdentity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var constr = "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=MyIdDb;integrated security=true;";
            builder.Services.AddDbContext<MyIdentityContext>(
                options => options.UseSqlServer(constr));

            builder.Services.AddIdentity<AppUser, AppRole>(
           options =>
            {
                 options.Password.RequireDigit = false;
                 options.Password.RequiredLength = 1;
                 options.Password.RequiredUniqueChars = 0;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireUppercase = false;
                 options.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<MyIdentityContext>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireRole("Admin")
                    .RequireAuthenticatedUser()
                    .Build();
            });
            builder.Services.ConfigureApplicationCookie(
                options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.LogoutPath = new PathString("/Account/Logout");
                    options.AccessDeniedPath = new PathString("/Home/AccessDenied");

                    options.Cookie = new()
                    {
                        Name = "Identity-Cerez",
                        HttpOnly = true,
                        SameSite = SameSiteMode.Lax,
                        SecurePolicy = CookieSecurePolicy.Always
                    };
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromDays(30);
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.Run();
        }
    }
}