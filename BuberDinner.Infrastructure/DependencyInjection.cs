using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Common.Services;
using BuberDinner.Infrastructure.Persistence;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using BuberDinner.Infrastructure.Persistence.Repositories;
using BuberDinner.Infrastructure.Persistence.Interceptors;

namespace BuberDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
            .AddAuth(configuration)
            .AddPersistance(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
    public static IServiceCollection AddPersistance(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<BuberDinnerDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();
        return services;
    }
    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);
        //can options vi dich vu lien quan co lien quan toi IOptions
        services.AddSingleton(Options.Create(jwtSettings));
        //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        //add dependency anh how to validate token
        //default scheme => jwtBear

        //AddAuthentication adding dependencies and return authenticationBuilder
        //authenticationBuilder has a map between schene and corresponding authentication handler
        //AddJwtBearer as authentication hanlder for the jwt bearer scheme => nhan parameter nhung thu muon validate
        //nhung thong so ben duoi nhu ValidateIssuer token duoc lay tu mot nguon dang tin cay => server
        //ValidateAudience token duoc phat cho mot Audience xac dinh
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
                
            });
        return services;
    }
}
