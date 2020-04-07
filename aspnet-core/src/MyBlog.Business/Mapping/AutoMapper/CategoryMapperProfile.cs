using AutoMapper;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;

namespace MyBlog.Business.Mapping.AutoMapper
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<CategoryTranslation, CategoryDto>().IncludeMembers(x => x.Language, x => x.Category);

            CreateMap<Language, CategoryDto>(MemberList.None);
            CreateMap<Category, CategoryDto>(MemberList.None);
        }
    }
}
