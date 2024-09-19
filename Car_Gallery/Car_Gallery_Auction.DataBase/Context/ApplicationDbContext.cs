using Car_Gallery_Auction.DataBase.Domain;
using Car_Gallery_Auction.DataBase.Modals;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Gallery_Auction.DataBase.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>  // default olan IdentityUser'ı kullanmadık bunun yerine daha esnek olması için bir class' üzerindendahil ettik.
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Bid> Bids { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<PaymentHistory> PaymentHistories { get; set; }


    }
}
