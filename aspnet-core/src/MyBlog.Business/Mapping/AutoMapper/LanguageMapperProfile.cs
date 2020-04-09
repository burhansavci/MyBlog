using AutoMapper;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Business.Mapping.AutoMapper
{
    public class LanguageMapperProfile : Profile
    {
        public LanguageMapperProfile()
        {
            CreateMap<Language, LanguageDto>();
            CreateMap<LanguageDto, Language>();
        }
    }
}
