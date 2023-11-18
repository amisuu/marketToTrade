using Application.Helpers;
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IErrorService, ErrorService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<ILikesService, LikesService>();
            services.AddScoped<IMessagesService, MessagesService>();
            services.AddScoped<LogUserActivity>();

            services.AddAutoMapper(typeof(MapperConfig).Assembly);

            return services;
        }
    }
}
