using Car_Gallery_Auction.DataBase.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Gallery_Auction.Business.Dtos
{
    public class CreateVehicleDTO 
    {

        public string BrandAndModal { get; set; }
        public int ManufacturingYear { get; set; }
        public string Color { get; set; }
        public decimal EngineCapacity { get; set; }
        public decimal Price { get; set; }
        public int Millage { get; set; }
        public string PlateNumber { get; set; }
        public double AuctionPrice { get; set; }  // açık arttırmaya giriş için ön alınan fiyat
        public string AdditionalInformation { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; }
        public Boolean IsActive { get; set; } = true;
        public string Image { get; set; }

        public string SellerID { get; set; }  // bir aracın tek satıcısı olabilir 

        public IFormFile File { get; set; }
    }
}
