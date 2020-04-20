using AutoMapper;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Business.Mapping.AutoMapper
{
    public class PictureMapperProfile : Profile
    {
        public PictureMapperProfile()
        {
            CreateMap<Picture, PictureForCreationDto>();
            CreateMap<PictureForCreationDto, Picture>();
        }
    }
}
