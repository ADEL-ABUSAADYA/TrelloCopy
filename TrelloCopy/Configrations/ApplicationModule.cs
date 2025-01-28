using System.Diagnostics;
using System.Reflection;
using System.Text;
using Autofac;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using TrelloCopy.Common;
using TrelloCopy.Common.Views;
using TrelloCopy.Data;
using TrelloCopy.Data.Repositories;
using TrelloCopy.Features.Common.Users.DTOs;
using TrelloCopy.Features.UserManagement.ConfirmUserRegistration;
using TrelloCopy.Features.UserManagement.RegisterUser;
using TrelloCopy.Features.userManagement.RegisterUser.Queries;
using TrelloCopy.Helpers;
using TrelloCopy.Models;
using Module = Autofac.Module;

namespace TrelloCopy.Configrations
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
             builder.Register(context =>
        {
            var config = context.Resolve<IConfiguration>();
            var connectionString = config.GetConnectionString("DefaultConnection");
            var options = new DbContextOptionsBuilder<Context>()
                .UseSqlServer(connectionString)
                .Options;

            return new Context(options);
        }).As<Context>().InstancePerLifetimeScope();

        // Register MediatR
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
               .AsClosedTypesOf(typeof(IRequestHandler<,>))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

        // Register validators
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
               .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

        // Register specific endpoint parameters
        builder.RegisterGeneric(typeof(BaseEndpointParameters<>))
               .AsSelf()
               .InstancePerLifetimeScope();

        // Register scoped endpoints
        builder.RegisterType<RegisterUserEndpoint>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<ConfirmEmailEndpoint>().AsSelf().InstancePerLifetimeScope();

        // Register repositories
        builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        builder.RegisterType<Repository<BaseModel>>().As<IRepository<BaseModel>>().InstancePerLifetimeScope();

        // Register base request handler parameters
        builder.RegisterType<UserBaseRequestHandlerParameters>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<BaseRequestHandlerParameters>().AsSelf().InstancePerLifetimeScope();

        // Register JWT authentication
        builder.Register(context =>
        {
            var config = context.Resolve<IConfiguration>();
            var jwtSettings = config.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetValue<string>("SecretKey");
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("SecretKey is not configured properly in appsettings.json");
            }

            var key = Encoding.UTF8.GetBytes(secretKey);

            return new JwtBearerOptions
            {
                TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                    ValidAudience = jwtSettings.GetValue<string>("Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                }
            };
        }).As<JwtBearerOptions>().SingleInstance();

        // Register HttpContextAccessor
        builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();

        // Register controllers, Swagger, etc.
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
               .Where(t => t.IsSubclassOf(typeof(ControllerBase)))
               .AsSelf()
               .InstancePerLifetimeScope();

        builder.RegisterType<SwaggerGenOptions>().AsSelf().SingleInstance();
        builder.RegisterType<ConfirmEmailRequestViewModelValidator>()
            .As<IValidator<ConfirmEmailRequestViewModel>>()
            .InstancePerLifetimeScope();
        builder.RegisterType<RegisterUserRequestViewModelValidator>()
            .As<IValidator<RegisterUserRequestViewModel>>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<TokenHelper>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<UserInfo>().AsSelf().InstancePerLifetimeScope();
        }
    }
}