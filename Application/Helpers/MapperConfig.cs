using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Helpers;

namespace Application.Helpers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url));
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
            CreateMap<AssetLike, AssetLikeDto>().ReverseMap();
            CreateMap<AppUserDto, AppUser>().ReverseMap();
            CreateMap<Asset, LikeDto>().ConstructUsing(x => new LikeDto());
            CreateMap<LikeDto, Asset>();
            CreateMap<LikeDto, Like>();
            CreateMap<Like, LikeDto>();
        }
    }
}
