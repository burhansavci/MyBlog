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
                var optionsBuilder = new DbContextOptionsBuilder<MyBlogDbContext>(dbContextOptions)
                    .UseApplicationServiceProvider(serviceProvider)
                    .UseSqlServer(configuration.GetConnectionString("MyBlogConnectionString"),
                        serverOptions => serverOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null));

                return optionsBuilder.Options;
            }).As<DbContextOptions<MyBlogDbContext>>()
              .InstancePerLifetimeScope();

            builder.Register(context => context.Resolve<DbContextOptions<MyBlogDbContext>>())
                   .As<DbContextOptions>()
                   .InstancePerLifetimeScope();

            builder.RegisterType(typeof(MyBlogDbContext))
                   .As(typeof(DbContext))
                   .InstancePerLifetimeScope();

            builder.RegisterType(typeof(MyBlogDbContext))
                   .AsSelf()
                   .InstancePerLifetimeScope();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterModule<AutoMapperModule>();

        }
    }
}
