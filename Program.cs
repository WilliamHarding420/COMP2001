
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace COMP2001 {

    public struct GenericResponse {

        public bool Success { get; set; }

        public string Message { get; set; }

        public GenericResponse(bool _Success, string _Message) {
            Success = _Success;
            Message = _Message;
        }

    }

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

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
