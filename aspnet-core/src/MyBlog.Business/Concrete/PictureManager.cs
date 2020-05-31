using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using MyBlog.Business.Abstract;
using MyBlog.Business.Constants;
using MyBlog.Business.Settings;
using MyBlog.Core.Utilities.Results;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyBlog.Business.Concrete
{
    public class PictureManager : IPictureService
    {
        private IMapper _mapper;
        private IPictureRepository _pictureRepository;
        private IArticleRepository _articleRepository;
        private CloudinarySettings _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PictureManager(IPictureRepository pictureRepository,
                              IArticleRepository articleRepository,
                              IMapper mapper,
                              IConfiguration configuration)
        {
            _pictureRepository = pictureRepository;
            _articleRepository = articleRepository;
            _mapper = mapper;
            _cloudinaryConfig = configuration.GetSection(AppSettingsSection.Cloudinary).Get<CloudinarySettings>();

            Account account = new Account(
                _cloudinaryConfig.CloudName,
                _cloudinaryConfig.APIKey,
                _cloudinaryConfig.APISecret);

            _cloudinary = new Cloudinary(account);
        }

        public IDataResult<PictureForReturnDto> GetPictureById(int id)
        {
            var picture = _pictureRepository.GetIncluding(x => x.Id == id);
            return new SuccessDataResult<PictureForReturnDto>(Messages.SuccessOperation, _mapper.Map<PictureForReturnDto>(picture));
        }

        public IDataResult<List<PictureForReturnDto>> GetPicturesByArticleId(int articleId)
        {
            var picture = _pictureRepository.GetAllIncludingList(x => x.ArticleId == articleId);
            return new SuccessDataResult<List<PictureForReturnDto>>(Messages.SuccessOperation, _mapper.Map<List<PictureForReturnDto>>(picture));
        }

        public IResult InsertPicturesForArticle(List<PictureForCreationDto> pictureForCreationDtos)
        {
            var mainPicture = pictureForCreationDtos.Find(x => x.IsMain);

            mainPicture ??= pictureForCreationDtos[0];

            var article = _articleRepository.GetIncluding(x => x.Id == mainPicture.ArticleId, x => x.Pictures);

            foreach (var pictureForCreationDto in pictureForCreationDtos)
            {
                var picture = UploadPictureToCloudinary(pictureForCreationDto);
                article.Pictures.Add(picture);
            }

            _articleRepository.Update(article);

            return new SuccessResult(string.Format(Messages.SuccessfulInsert, nameof(Picture)));
        }
        public IResult InsertPictureForArticle(PictureForCreationDto pictureForCreationDto)
        {
            var article = _articleRepository.GetIncluding(x => x.Id == pictureForCreationDto.ArticleId, x => x.Pictures);

            var picture = UploadPictureToCloudinary(pictureForCreationDto);

            _pictureRepository.Insert(picture);

            return new SuccessResult(string.Format(Messages.SuccessfulInsert, nameof(Picture)));
        }
        private Picture UploadPictureToCloudinary(PictureForCreationDto pictureForCreationDto)
        {
            var file = pictureForCreationDto.File;
            ImageUploadResult uploadResult = new ImageUploadResult();
            if (file?.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    if (pictureForCreationDto.IsMain)
                        uploadParams.Transformation = new Transformation().Width(1200).Height(600).Crop("fill").Gravity("face");

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            pictureForCreationDto.Url = uploadResult.Uri.ToString();
            pictureForCreationDto.PublicId = uploadResult.PublicId;

            var picture = _mapper.Map<Picture>(pictureForCreationDto);

            return picture;
        }

        public IResult DeletePicture(PictureForDeleteDto pictureForDeleteDto)
        {
            var pictureToBeDeleted = _mapper.Map<Picture>(pictureForDeleteDto);

            var deletionParams = new DeletionParams(pictureToBeDeleted.PublicId);
            var result = _cloudinary.Destroy(deletionParams);
            if (result.Result == "ok")
            {
                _pictureRepository.Delete(pictureToBeDeleted);
            }
            return new SuccessResult(string.Format(Messages.SuccessfulDelete, nameof(Picture)));
        }

    }
}
