using Microsoft.EntityFrameworkCore;
using SoleMates.Models;

namespace SoleMates
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<myContext>(options=>options.UseSqlServer(
                builder.Configuration.GetConnectionString("myconnection")));

            builder.Services.AddSession(options=>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(240);
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

            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=customer}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
