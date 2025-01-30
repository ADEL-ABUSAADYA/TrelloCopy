using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TrelloCopy.Configrations;
using TrelloCopy.Middlewares;

namespace TrelloCopy;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(container => { container.RegisterModule<ApplicationModule>(); });
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("SecretKey"));

        builder.Services.AddAuthentication(opts => {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                    ValidAudience = jwtSettings.GetValue<string>("Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                };
            })
            .AddJwtBearer("2FA", opts =>
            {
                var otpKey = Encoding.ASCII.GetBytes(jwtSettings.GetValue<string>("OTPSettings:SecretKey"));
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.GetValue<string>("OTPSettings:Issuer"),
                    ValidAudience = jwtSettings.GetValue<string>("OTPSettings:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(otpKey),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                };
            });
        builder.Services.AddAuthorization();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
        builder.Services.AddMediatR(typeof(Program).Assembly);
        builder.Services.AddControllersWithViews(opt => opt.Filters.Add<UserInfoFilter>());
        
        var app = builder.Build();
        app.UseAuthentication();
        app.UseAuthorization();
        if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.UseMiddleware<GlobalErrorHandlerMiddleware>();
        app.UseMiddleware<TransactionMiddleware>();
        app.Run();
        

        
        

        
    }
}