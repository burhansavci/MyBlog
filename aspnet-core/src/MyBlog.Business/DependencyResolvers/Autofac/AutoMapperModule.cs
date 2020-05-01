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
                    x.AddProfile(new ArticleMapperProfile());
                    x.AddProfile(new CommentMapperProfile());
                    x.AddProfile(new PictureMapperProfile());
                });

                return config;
            }).SingleInstance() // We only need one instance
                .AutoActivate() // Create it on ContainerBuilder.Build()
                .AsSelf(); // Bind it to its own type

            builder.Register(context =>
            {
                var config = context.Resolve<MapperConfiguration>();
                // Create our mapper using our configuration above
                return config.CreateMapper(t => context.Resolve(t));
            }).As<IMapper>(); // Bind it to the IMapper interface

            base.Load(builder);
        }
    }
}
