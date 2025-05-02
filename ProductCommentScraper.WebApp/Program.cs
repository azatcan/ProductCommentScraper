using AspNetCore.Identity.Mongo.Model;
using AspNetCore.Identity.Mongo;
using Microsoft.Extensions.Options;
using ProductCommentScraper.Domain.Application.GeneralMappings;
using ProductCommentScraper.Domain.Application.Options;
using ProductCommentScraper.Domain.Application.Options.Abstraction;
using ProductCommentScraper.Domain.Repositories;
using ProductCommentScraper.Domain.Services;
using ProductCommentScraper.Infrastructure.Persistence.DataContext;
using ProductCommentScraper.Infrastructure.Repositories;
using ProductCommentScraper.Infrastructure.Services;
using ProductCommentScraper.Domain.Entities;

namespace ProductCommentScraper.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("DatabaseOptions"));
            builder.Services.AddSingleton<IDatabaseOptions>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;

            });

            builder.Services.AddScoped<MongoDbContext>();
            builder.Services.AddAutoMapper(typeof(GeneralMapping));
            builder.Services.AddScoped<IProductRepository,ProductRepository>();
            builder.Services.AddScoped<IProductService,ProductManager>();
            builder.Services.AddScoped<IProductScraperService, ProductScraperService>();

            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IReviewService,ReviewManager>();


            builder.Services.AddScoped<IProductFeatureRepository, ProductFeatureRepository>();
            builder.Services.AddScoped<IProductFeatureService, ProductFeatureManager>();


            builder.Services.AddScoped<ISellSourceRepository, SellSourceRepository>();
            builder.Services.AddScoped<ISellSourceService, SellSourceManager>();


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

            app.UseAuthorization();

            app.MapControllerRoute(

                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
