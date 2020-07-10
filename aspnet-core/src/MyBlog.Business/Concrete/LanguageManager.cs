using AutoMapper;
using AutoMapper.QueryableExtensions;
using MyBlog.Business.Abstract;
using MyBlog.Business.Constants;
using MyBlog.Core.Entities.Dtos;
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
            return new SuccessDataResult<LanguageDto>(Messages.SuccessOperation, _mapper.Map<LanguageDto>(language));
        }

        public IDataResult<List<LanguageDto>> GetLanguages() { 
            var languages = _languageRepository.GetAllList();
            return new SuccessDataResult<List<LanguageDto>>(Messages.SuccessOperation, _mapper.Map<List<LanguageDto>>(languages));
        }

        public IDataResult<List<Page<LanguageDto>>> GetPaginatedLanguages(int startPageNumber, int endPageNumber, int pageSize)
        {
            var languages = _languageRepository.GetAll().ProjectTo<LanguageDto>(_mapper.ConfigurationProvider);
            var languageDtos =Page<LanguageDto>.CreatePaginatedResultList(languages, startPageNumber, endPageNumber, pageSize);
            return new SuccessDataResult<List<Page<LanguageDto>>>(Messages.SuccessOperation, languageDtos);
        }

        public IResult InsertLanguage(LanguageDto languageDto)
        {
            var languageToBeInserted = _mapper.Map<Language>(languageDto);
            _languageRepository.Insert(languageToBeInserted);
            return new SuccessResult(string.Format(Messages.SuccessfulInsert, nameof(Language)));
        }

        public IResult UpdateLanguage(LanguageDto languageDto)
        {
            var languageToBeUpdated = _mapper.Map<Language>(languageDto);
            _languageRepository.Update(languageToBeUpdated);
            return new SuccessResult(string.Format(Messages.SuccessfulUpdate, nameof(Language)));
        }
        public IResult DeleteLanguage(LanguageDto languageDto)
        {
            var languageToBeDeleted = _mapper.Map<Language>(languageDto);
            _languageRepository.Delete(languageToBeDeleted);
            return new SuccessResult(string.Format(Messages.SuccessfulDelete, nameof(Language)));
        }
    }
}
