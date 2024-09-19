using Car_Gallery_Auction.DataBase.Modals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Car_Gallery_Auction.DataBase.Domain
{
    public class Vehicle
    {


        [Key]
        public int VehicleID { get; set; }

        public string BrandAndModal { get; set; }
        public int ManufacturingYear { get; set; }
        public string  Color { get; set; }
        public decimal EngineCapacity { get; set; }
        public decimal Price { get; set; }
        public int Millage { get; set; }
        public string PlateNumber { get; set; }
        public double AuctionPrice { get; set; }  // açık arttırmaya giriş için ön alınan fiyat
        public string AdditionalInformation { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Boolean IsActive { get; set; }
        public string Image { get; set; }

        public string SellerID { get; set; }  // bir aracın tek satıcısı olabilir 

        [JsonIgnore]
        public ApplicationUser Seller { get; set; }

        public virtual List<Bid> Bids { get; set; } // aracın birden fazla fiyatı olabilir 

    }
}
