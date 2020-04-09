using AutoMapper;
using MyBlog.Business.Abstract;
using MyBlog.Core.Utilities.Results;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using System;
using System.Collections.Generic;

namespace MyBlog.Business.Concrete
{
    public class LanguageManager : ILanguageService
    {
        private ILanguageRepository _languageRepository;
        private IMapper _mapper;
        public LanguageManager(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }
        public IDataResult<LanguageDto> GetLanguageByCode(string languageCode)
        {
            var language = _languageRepository.Get(x => x.LanguageCode == languageCode);
            return new SuccessDataResult<LanguageDto>(_mapper.Map<LanguageDto>(language));
        }

        public IDataResult<List<LanguageDto>> GetLanguages()
        {
            var languages = _languageRepository.GetAllList();
            return new SuccessDataResult<List<LanguageDto>>(_mapper.Map<List<LanguageDto>>(languages));
        }

        public IResult InsertLanguage(LanguageDto languageDto)
        {
            var languageToBeInserted = _mapper.Map<Language>(languageDto);
            _languageRepository.Insert(languageToBeInserted);
            return new SuccessResult();
        }

        public IResult UpdateLanguage(LanguageDto languageDto)
        {
            var languageToBeUpdated = _mapper.Map<Language>(languageDto);
            _languageRepository.Update(languageToBeUpdated);
            return new SuccessResult();
        }
        public IResult DeleteLanguage(LanguageDto languageDto)
        {
            var languageToBeDeleted = _mapper.Map<Language>(languageDto);
            _languageRepository.Delete(languageToBeDeleted);
            return new SuccessResult();
        }
    }
}
