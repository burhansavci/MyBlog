using Autofac;
using Microsoft.EntityFrameworkCore;
using MyBlog.Core.Utilities.Security.Jwt;
using MyBlog.DataAccess.Concrete.EntityFrameworkCore;
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

            builder.RegisterType(typeof(MyBlogDbContext)).As(typeof(DbContext)).InstancePerLifetimeScope();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterModule<AutoMapperModule>();

        }
    }
}
