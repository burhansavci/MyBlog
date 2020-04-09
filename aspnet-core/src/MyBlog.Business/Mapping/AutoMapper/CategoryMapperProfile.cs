using AutoMapper;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using System;

namespace MyBlog.Business.Mapping.AutoMapper
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<CategoryTranslation, CategoryDto>().IncludeMembers(x => x.Category);
            CreateMap<Category, CategoryDto>(MemberList.None);


            CreateMap<CategoryTranslation, CategoryDto>()
                .IncludeMembers( x => x.Category)
                .ReverseMap();
            CreateMap<CategoryDto, Category>(MemberList.None);
        }
    }
}
