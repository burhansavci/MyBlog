using AutoMapper;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;

namespace MyBlog.Business.Mapping.AutoMapper
{
    public class LanguageMapperProfile : Profile
    {
        public LanguageMapperProfile()
        {
            CreateMap<Language, LanguageDto>()
                .ForMember(x => x.LanguageCode, opt => opt.MapFrom(x => x.LanguageCode.Trim()));
            CreateMap<LanguageDto, Language>();
        }
    }
}
