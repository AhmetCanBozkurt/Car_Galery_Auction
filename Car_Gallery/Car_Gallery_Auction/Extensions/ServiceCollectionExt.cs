using Car_Gallery_Auction.Business.Abstract;
using Car_Gallery_Auction.Business.Concrete;
using Car_Galllery_Auction.Core.Modals;

namespace Car_Gallery_Auction.Extensions
{
    public static class ServiceCollectionExt
    {

        public static IServiceCollection AddApplicationLayer(this IServiceCollection services,IConfiguration configuration)
        {

            #region  services

            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IUserService, UserService>(); // dependecy Injection için yapıyoruz yönlendirmesi adına bunu Ninject ilede yapabilirdik.
            services.AddScoped(typeof(ApiResponse));
            #endregion


            return services;
        }
    }
}
