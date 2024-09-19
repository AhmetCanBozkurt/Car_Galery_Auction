using Car_Gallery_Auction.DataBase.Modals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Gallery_Auction.DataBase.Domain
{
    public class PaymentHistory
    {

        [Key]
        public int PaymentId { get; set; }

        public string UserId { get; set; }
        public ApplicationUser  User { get; set; }

        public Boolean IsActive { get; set; }

        public DateTime PayDate { get; set; } // ödeme tarihi önemlidir.

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

    }
}
