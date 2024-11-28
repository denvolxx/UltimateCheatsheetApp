using DBService.Data;
using DBService.Extensions;
using DBService.Services.AccountService;
using DBService.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDBService.Data;
using MongoDBService.Services;
using System.Text;
using UltimateCheatsheetApp.Extensions;
using UltimateCheatsheetApp.Middlewares;

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

            // Swagger. Configured in separate extension method. Too much code for this page.
            builder.Services.AddSwaggerService();

            //Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenKey").Value!)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Mappers
            // AddAutoMapperProfiles - extension method for DBService mappers
            builder.Services.AddAutoMapperProfiles();

            // Data access services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IAccountService, AccountService>();

            //CORS
            builder.Services.AddCors();
            #endregion

            var app = builder.Build();

            #region Web Application Configurations

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cheatsheet API");
                });
            }

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:7202", "http://localhost:5084"));

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
