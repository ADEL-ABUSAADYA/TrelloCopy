using System.Diagnostics;
using Autofac;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Data;
using TrelloCopy.Data.Repositories;
using TrelloCopy.Helpers;


namespace TrelloCopy.Configrations;

public class AutoFacModule: Module
{
    public AutoFacModule()
    {

    }
    
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(context =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            var configuration = context.Resolve<IConfiguration>();
            

            optionsBuilder
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(Context).Assembly.FullName))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                .EnableSensitiveDataLogging();

            return new Context(optionsBuilder.Options);
        }).AsSelf().InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(Program).Assembly)
            .Where(c => c.Name.EndsWith("Service") || c.Name.EndsWith("Repository"))
            .AsImplementedInterfaces().InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(Program).Assembly)
            .Where(c => c.Name.EndsWith("Mediator"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(Program).Assembly)
            .Where(c => c.Name.EndsWith("Accessor"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterType<TokenHelper>().AsSelf().InstancePerLifetimeScope();
    }
}
