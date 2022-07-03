using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Helpers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AppUser, MemberDto>();
            CreateMap<UpdateMemberDto, AppUser>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<Asset, AssetDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<Photo, PhotoDto>();
            CreateMap<PhotoDto, Photo>();
            CreateMap<AssetDto, Asset>();
            CreateMap<UpdateAssetDto, Asset>();
            CreateMap<Asset, AddNewAssetDto>();
            CreateMap<AddNewAssetDto, Asset>();
            CreateMap<AssetLike, AssetLikeDto>();
            CreateMap<AssetDto, AssetLikeDto>();
            CreateMap<AssetLikeDto, AssetDto>();
        }
    }
}
