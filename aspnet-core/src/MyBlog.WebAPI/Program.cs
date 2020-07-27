using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MyBlog.Business.DependencyResolvers.Autofac;
using MyBlog.DataAccess.Concrete.EntityFrameworkCore;
using MyBlog.DataAccess.Concrete.EntityFrameworkCore.Seed;
using System;
using System.Threading.Tasks;

namespace MyBlog.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.GetAutofacRoot().BeginLifetimeScope())
            {
                var context = scope.Resolve<MyBlogDbContext>();
                try
                {
                    await context.Database.MigrateAsync();
                    await MyBlogDbContextSeed.SeedUsersAsync(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occured during migration {ex}");
                } 
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutofacBusinessModule());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
