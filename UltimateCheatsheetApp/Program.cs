using DBService.Data;
using DBService.Services.UserService;
using Microsoft.EntityFrameworkCore;
using MongoDBService.Data;
using MongoDBService.Services;

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

            //MongoDB
            builder.Services.AddScoped<MongoDataContext>(_ => new MongoDataContext(builder.Configuration.GetConnectionString("MongoConnection"),
                "CheatsheetAppMongo"));

            // Controllers
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Mapper

            // Data access services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProductService, ProductService>();

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
