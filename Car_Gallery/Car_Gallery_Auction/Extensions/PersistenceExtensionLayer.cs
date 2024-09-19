using Car_Gallery_Auction.Business.Abstract;
using Car_Gallery_Auction.Business.Concrete;
using Car_Gallery_Auction.DataBase.Context;
using Car_Gallery_Auction.DataBase.Modals;
using Car_Galllery_Auction.Core.Modals;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Car_Gallery_Auction.Extensions
{
    public static class PersistenceExtensionLayer
    {

        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {

            #region  Context

            // Burada dependecy Injection ile default olarak ayarlardan hangi veritabanı bağlantısına gideceğine dair bilgisini verdik.
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            #endregion


            return services;
        }
    }
}
