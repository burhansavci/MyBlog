using Autofac;
using MyBlog.DataAccess.Concrete.EntityFrameworkCore;
using System.Linq;

namespace MyBlog.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<MyBlogDbContext>().AsSelf().InstancePerRequest();

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load(nameof(DataAccess)))
                   .Where(t => t.Namespace.Contains("Repositories"))
                   .As(t => t.GetInterfaces()[0]);

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load(nameof(Business)))
                   .Where(t => t.Name.EndsWith("Manager"))
                   .AsImplementedInterfaces();

        }
    }
}
