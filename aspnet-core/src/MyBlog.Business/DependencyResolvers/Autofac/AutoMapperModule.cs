using Autofac;
using AutoMapper;
using MyBlog.Business.Mapping.AutoMapper;

namespace MyBlog.Business.DependencyResolvers.Autofac
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var config = new MapperConfiguration(x =>
                {
                    x.AddProfile(new CategoryMapperProfile());
                    x.AddProfile(new LanguageMapperProfile());
                });

                return config;
            }).SingleInstance() // We only need one instance
                .AutoActivate() // Create it on ContainerBuilder.Build()
                .AsSelf(); // Bind it to its own type

            // HACK: IComponentContext needs to be resolved again as 'tempContext' is only temporary. See http://stackoverflow.com/a/5386634/718053 
            builder.Register(tempContext =>
            {
                var ctx = tempContext.Resolve<IComponentContext>();
                var config = ctx.Resolve<MapperConfiguration>();

                // Create our mapper using our configuration above
                return config.CreateMapper(t => ctx.Resolve(t));
            }).As<IMapper>(); // Bind it to the IMapper interface

            base.Load(builder);
        }
    }
}
