﻿using AutoMapper;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;

namespace MyBlog.Business.Mapping.AutoMapper
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {

            CreateMap<CategoryTranslation, CategoryForReturnDto>()
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => s.Category.CreatedDate))
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryId))
                .ForMember(d => d.CategoryTranslationId, opt => opt.MapFrom(s => s.Id));

            CreateMap<CategoryForDeleteDto, CategoryTranslation>()
                .ForMember(d => d.CategoryId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryTranslationId));

            CreateMap<CategoryForUpdateDto, CategoryTranslation>()
                .ForPath(d => d.Category.CreatedDate, opt => opt.MapFrom(s => s.CreatedDate))
                .ForPath(d => d.Category.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryTranslationId));

            CreateMap<CategoryForCreationDto, CategoryTranslation>()
               .ForPath(d => d.Category.CreatedDate, opt => opt.MapFrom(s => s.CreatedDate));
        }
    }
}
