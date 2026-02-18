using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Restaurants.Api.Exceptions;
using Restaurants.Api.Extensions;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Resturants.Domain.Entities;
using Serilog;


namespace UdemyCourse_Jakob_Kozera
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.AddPresentation();
            builder.Services.AddApplication();
            builder.Services.AddInsfrastructure(builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();


            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
            await seeder.Seed();

            app.UseAuthorization();
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.MapGroup("api/identity").MapIdentityApi<User>().WithTags("Identity");
            app.MapControllers();

            app.Run();
        }
    }
}
