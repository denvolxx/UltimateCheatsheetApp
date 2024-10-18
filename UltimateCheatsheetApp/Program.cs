using DBService.Data;
using DBService.Services.UserService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace UltimateCheatsheetApp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            #region Service Configurations

            // DB context. Migrations added for DBService project. Hint https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=vs
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("DBService")
                )
            );

            //options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

            // Controllers
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Mapper

            // Data access services
            builder.Services.AddScoped<IUserService, UserService>();

            #endregion

            var app = builder.Build();

            #region Web Application Configurations

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
