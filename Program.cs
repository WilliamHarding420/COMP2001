
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace COMP2001 {

    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Profile API",
                    Version = "v1"
                });

                string xmlPath = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                xmlPath = Path.Combine(AppContext.BaseDirectory, xmlPath);

                options.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();
            Database.app = app;

            app.UseHttpsRedirection();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                if (!app.Environment.IsDevelopment())
                    options.SwaggerEndpoint("/COMP2001/WHarding/swagger/v1/swagger.json", "Profile API");
            });

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
