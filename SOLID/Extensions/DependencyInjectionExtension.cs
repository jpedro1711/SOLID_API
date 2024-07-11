using SOLID.Data;
using SOLID.Repositories.Interfaces;
using SOLID.Repositories;
using SOLID.Services.interfaces;
using SOLID.Services;
using SOLID.Transactions;
using SOLID.UseCases.Interfaces;
using SOLID.UseCases.Strategies.Factories;
using SOLID.UseCases;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SOLID.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAppDbContext, SalaryAppDbContext>();
            services.AddScoped<IPayrollRepository, PayrollRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICalculatePayroll, CalculatePayroll>();
            services.AddScoped<ICalculateSalaryFactoryMethod, CalculateSalaryFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRegisterUser, RegisterUser>();
            services.AddScoped<IAuthenticateUser, AuthenticateUser>();
            services.AddScoped<ITokenService, TokenService>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options => {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        public static void InitMigration(WebApplication app)
        {
            try
            {
                InitDB.InitDb(app);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error seeding DB");
            }

        }

        public static void ApplyMigration(WebApplication app)
        {
            void ApplyMigration()
            {
                using (var scope = app.Services.CreateScope())
                {
                    var _db = scope.ServiceProvider.GetRequiredService<SalaryAppDbContext>();

                    if (_db.Database.GetPendingMigrations().Count() > 0)
                    {
                        _db.Database.Migrate();
                    }
                }
            }
        }

        public static void ConfigureAuth(WebApplicationBuilder builder)
        {
            var appSettings = builder.Configuration.GetSection("AppSettings");
            var token = appSettings["Token"];

            builder.Services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });
        }
    }
}
