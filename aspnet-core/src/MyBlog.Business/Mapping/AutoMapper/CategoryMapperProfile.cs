using AutoMapper;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;

namespace MyBlog.Business.Mapping.AutoMapper
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<CategoryTranslation, CategoryDto>()
                .IncludeMembers(x => x.Category);

            CreateMap<Category, CategoryDto>(MemberList.None);


            CreateMap<CategoryDto, Category>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryId));

            CreateMap<CategoryDto, CategoryTranslation>()
                .ForPath(d => d.Category.CreatedDate, opt => opt.MapFrom(s => s.CreatedDate))
                .ForPath(d => d.Category.Id, opt => opt.MapFrom(s => s.CategoryId))
                .AfterMap((s, d) =>
                {
                    if (s.CategoryId != null)
                        d.Category = null;
                });
        }
    }
}
