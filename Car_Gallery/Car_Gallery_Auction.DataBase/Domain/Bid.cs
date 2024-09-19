using Car_Gallery_Auction.DataBase.Enums;
using Car_Gallery_Auction.DataBase.Modals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Car_Gallery_Auction.DataBase.Domain
{
    public class Bid
    {

        [Key]
        public int BidId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidDate { get; set; }

        public string BidStatus { get; set; } = Car_Gallery_Auction.DataBase.Enums.BidStatus.Pending.ToString();

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }  



        public int VecihleId { get; set; } // Bid sadece bir aracı ilgilendirir
        public Vehicle Vehicle { get; set; }
    }
}
