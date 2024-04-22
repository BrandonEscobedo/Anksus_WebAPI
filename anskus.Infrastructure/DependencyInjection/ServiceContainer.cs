using anskus.Application.Contracts;
using anskus.Application.Data;
using anskus.Application.Services;
using anskus.Infrastructure.Repository;
using anskus.Infrastructure.Repository.CuestionariosServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using anskus.Domain;
using anskus.Domain.Cuestionarios;
using anskus.Application.DependencyInjection;
using anskus.Infrastructure.Data;
using anskus.Domain.Authentication;
using anskus.Infrastructure.Repository.CuestionarioActivoServices;
using anskus.Infrastructure.Repository.RandomCode;
namespace anskus.Infrastructure.DependencyInjection
{
    public  static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<AppicationAssemblyReference>();
            });

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
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IAccountUser, AccountRepository>();
            services.AddScoped<IDbContext>(sp=>sp.GetRequiredService<TestAnskusContext>());
            services.AddScoped<IUnitOfWork>(sp=>sp.GetRequiredService<TestAnskusContext>());
            services.AddScoped<ICuestionarioRepository, CuestionarioRepository>();
            services.AddScoped<ICuestionarioActivoRepository, CuestionarioActivoRepository>();
            services.AddScoped<IPreguntasRepository, PreguntasRepository>();
            services.AddScoped<IRespuestasRepository, RespuestasRepository>();
            services.AddScoped<IRandomCodeRepository, RandomCodeRepository>();
            return services;
        }
    }
}
