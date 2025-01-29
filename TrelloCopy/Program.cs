using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using FoodApp.Api.Settings;
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
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
        builder.Services.AddMediatR(typeof(Program).Assembly);
        builder.Services.AddAuthorization();
        builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
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

       // app.UseMiddleware<GlobalErrorHandlerMiddleware>();
        
       app.UseMiddleware<TransactionMiddleware>();
        
        app.Run();

    }
}

// builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//
