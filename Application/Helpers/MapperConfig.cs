using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Helpers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<Asset, AssetDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src => src.Sender != null ? 
                src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url : null)) //In Entity there is no SenderPhotoUrl
                .ForMember(dest => dest.ReceipientPhotoUrl, opt => opt.MapFrom(src => src.Receipient != null ?
                src.Receipient.Photos.FirstOrDefault(x => x.IsMain).Url : null));
            CreateMap<UpdateMemberDto, AppUser>();
            CreateMap<RegisterDto, AppUser>();
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
