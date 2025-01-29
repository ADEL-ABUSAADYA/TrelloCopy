using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TrelloCopy.Common;
using TrelloCopy.Configrations;
using TrelloCopy.Data;
using TrelloCopy.Data.Repositories;
using TrelloCopy.Features.UserManagement.ConfirmUserRegistration;
using TrelloCopy.Features.UserManagement.RegisterUser;
using TrelloCopy.Middlewares;
using TrelloCopy.Models;

namespace TrelloCopy;

public class Program
{
    public static void Main(string[] args)
    { 
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(
            container =>
            {
                container.RegisterModule<ApplicationModule>();
                container.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                    .AsClosedTypesOf(typeof(IValidator<>))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
                container.RegisterGeneric(typeof(BaseEndpointParameters<>))
                    .AsSelf()
                    .InstancePerLifetimeScope();
                container.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            });
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("SecretKey"));

        builder.Services.AddAuthentication(opts =>
            {
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
            });
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddAuthorization();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
        builder.Services.AddMediatR(typeof(Program).Assembly);
        builder.Services.AddAuthorization();

        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseMiddleware<GlobalErrorHandlerMiddleware>();
        
        app.UseMiddleware<TransactionMiddleware>();
        
        app.Run();

    }
}

// builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//
