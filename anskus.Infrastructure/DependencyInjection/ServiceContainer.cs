using Anksus_WebAPI.Models.dbModels;
using anskus.Application.Contracts;
using anskus.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace anskus.Infrastructure.DependencyInjection
{
    public  static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestAnskusContext>(o => o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<TestAnskusContext>()
                .AddSignInManager();
            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JwtIssuer"],
            ValidAudience = configuration["JwtAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]!))
        };
    });
            services.AddAuthentication();
            services.AddAuthorization();

            services.AddScoped<IAccountUser, AccountRepository>();
            return services;
        }
    }
}
