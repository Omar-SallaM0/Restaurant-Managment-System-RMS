using Microsoft.OpenApi.Models;
using Restaurants.Api.Exceptions;
using Serilog;

namespace Restaurants.Api.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication();
            builder.Services.AddControllers().AddJsonOptions(op => {
                op.JsonSerializerOptions.IncludeFields = true;
                op.JsonSerializerOptions.ReferenceHandler =
                     System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="bearerAuth"
                            },
                            Name ="Bearer",
                            In=ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<ErrorHandlingMiddleware>();

            Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

            builder.Host.UseSerilog();
         
        }
    }
}
