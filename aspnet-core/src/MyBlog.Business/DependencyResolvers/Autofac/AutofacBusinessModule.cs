using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using MyBlog.Core.Utilities.Security.Jwt;
using MyBlog.DataAccess.Concrete.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBlog.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load("MyBlog.DataAccess"))
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load("MyBlog.Business"))
                   .Where(t => t.Name.EndsWith("Manager"))
                   .AsImplementedInterfaces();

            builder.Register(componentContext =>
            {
                var serviceProvider = componentContext.Resolve<IServiceProvider>();
                var configuration = componentContext.Resolve<IConfiguration>();
                var dbContextOptions = new DbContextOptions<MyBlogDbContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                string connectionString;
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    // Use connection string from file.
                    connectionString = configuration.GetConnectionString("MyBlogConnectionString");
                }
                else
                {
                    // Use connection string provided at runtime by Heroku.
                    var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                    // Parse connection URL to connection string for Npgsql
                    connectionUrl = connectionUrl.Replace("postgres://", string.Empty);
                    var pgUserPassword = connectionUrl.Split("@")[0];
                    var pgHostPortDb = connectionUrl.Split("@")[1];
                    var pgHostPort = pgHostPortDb.Split("/")[0];
                    var pgDb = pgHostPortDb.Split("/")[1];
                    var pgUser = pgUserPassword.Split(":")[0];
                    var pgPassword = pgUserPassword.Split(":")[1];
                    var pgHost = pgHostPort.Split(":")[0];
                    var pgPort = pgHostPort.Split(":")[1];
                    connectionString = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPassword};Database={pgDb}";
                }


                var optionsBuilder = new DbContextOptionsBuilder<MyBlogDbContext>(dbContextOptions)
                    .UseApplicationServiceProvider(serviceProvider)
                    .UseNpgsql(connectionString,serverOptions => serverOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null));

                return optionsBuilder.Options;
            }).As<DbContextOptions<MyBlogDbContext>>()
              .InstancePerDependency();

            builder.Register(context => context.Resolve<DbContextOptions<MyBlogDbContext>>())
                   .As<DbContextOptions>()
                   .InstancePerDependency();

            builder.RegisterType(typeof(MyBlogDbContext))
                   .As(typeof(DbContext))
                   .InstancePerDependency();

            builder.RegisterType(typeof(MyBlogDbContext))
                   .AsSelf()
                   .InstancePerDependency();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterModule<AutoMapperModule>();

        }
    }
}
