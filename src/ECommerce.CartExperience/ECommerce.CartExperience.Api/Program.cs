using ECommerce.CartExperience.Api.Data;
using ECommerce.CartExperience.Api.Repositories;
using ECommerce.CartExperience.Api.Repositories.Interfaces;
using ECommerce.CartExperience.Api.Services;
using ECommerce.CartExperience.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.CartExperience.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<ICartExperienceService, CartExperienceService>();
            builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<IItemRepository, ItemRepository>();

            builder.Services.AddDbContext<DataContext>(options =>
                options.UseInMemoryDatabase("CartDb"));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
