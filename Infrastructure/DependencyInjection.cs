using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IErrorRepository, ErrorRepository>();
            services.AddScoped<IMemberRepository, UserRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<ILikesRepository, LikesRepository>();

            return services;
        }
    }
}
