using Application.DTOs;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(PhotoDto photoDto, AppUserDto userDto);

        Task<DeletionResult> DeletePhotoAsync2(string publicId);
        Task<PhotoDto> GetNewPhotoResult(ImageUploadResult result, string username, AssetDto assetDto);
    }
}
