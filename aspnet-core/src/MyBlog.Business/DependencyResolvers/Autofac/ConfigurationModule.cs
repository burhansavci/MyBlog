using Autofac;
using Microsoft.Extensions.Configuration;
using System;

namespace MyBlog.Business.DependencyResolvers.Autofac
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(context =>
            {
                return (ConfigurationRoot)new ConfigurationBuilder()
                                             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                             .AddJsonFile("appsettings.json")
                                             .Build();
            }).SingleInstance() // We only need one instance
              .AutoActivate() // Create it on ContainerBuilder.Build()
              .AsSelf(); // Bind it to its own type


            builder.Register(context => context.Resolve<ConfigurationRoot>())
                   .As<IConfigurationRoot>(); // Bind it to the IConfigurationRoot interface

            base.Load(builder);

        }

    }
}
