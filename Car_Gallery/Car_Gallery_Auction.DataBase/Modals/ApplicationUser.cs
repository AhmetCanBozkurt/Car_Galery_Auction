using Car_Gallery_Auction.DataBase.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Gallery_Auction.DataBase.Modals
{
    public class ApplicationUser : IdentityUser
    {

        public string?  FullName { get; set; }
        public string? ProfilePicture { get; set; }

        public DateTime DateofBirth { get; set; }

        public ICollection<PaymentHistory> PaymentHistories { get; set; }  // birden fazla açık arttırmaya girebilir

        public ICollection<Vehicle>  Vehicles{ get; set; }
        public ICollection<Bid> Bid { get; set; }
    }
}
