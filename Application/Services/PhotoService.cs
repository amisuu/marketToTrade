using Application.DTOs;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    internal class PhotoService : IPhotoService
    {
        public readonly Cloudinary _cloudinary;
        private readonly IMapper _mapper;
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetService _assetService;
        private readonly ILogger<PhotoService> _logger;

        public PhotoService(IOptions<CloudinarySettings> config,
                            IMapper mapper,
                            IAssetRepository assetRepository,
                            IAssetService assetService,
                            ILogger<PhotoService> logger)
        {
            var account = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
            _mapper = mapper;
            _assetRepository = assetRepository;
            _assetService = assetService;
            _logger = logger;
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(PhotoDto photoDto, AppUserDto userDto)
        {
            var deleteParams = new DeletionParams(photoDto.PublicId);
            
            userDto.Photos.Remove(photoDto);

            return await _cloudinary.DestroyAsync(deleteParams);
        }

        public async Task<DeletionResult> DeletePhotoAsync2(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);


            return await _cloudinary.DestroyAsync(deleteParams);
        }

        public async Task<PhotoDto> GetNewPhotoResult(ImageUploadResult result, AppUserDto user, AssetDto assetDto)
        {
            var photoDto = new PhotoDto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
            };

            if (user != null)
            {
                if (user.Photos.Count == 0)
                    photoDto.IsMain = true;

                user.Photos.Add(photoDto);
            }
            else if (assetDto != null)
            {
                if (assetDto.Photos.Count == 0)
                    photoDto.IsMain = true;

                var asset = await _assetRepository.GetAssetById(assetDto.Id);
                asset.Photos.Add(_mapper.Map<Photo>(photoDto));
            }

            return photoDto;
        }
    }
}
