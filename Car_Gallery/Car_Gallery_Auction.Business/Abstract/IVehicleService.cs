using Car_Gallery_Auction.Business.Dtos;
using Car_Galllery_Auction.Core.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Gallery_Auction.Business.Abstract
{
    public interface IVehicleService
    {

        Task<ApiResponse> CreateVehicle(CreateVehicleDTO model);

        Task<ApiResponse> GetVehicles(); //araçları çeken bir servis olacak

        Task<ApiResponse> UpdateVehicleResponse(int vehicleId,UpdateVehicleDTO model); // güncelleme yapacağımız metod

        Task<ApiResponse> DeleteVehicle(int vehicleId); //araci silen metot

        Task<ApiResponse> GetVehicleById(int vehicleId); //aracin detayını getirmek için

        Task<ApiResponse> ChangeVehicleStatus(int vehicleId); //aracin statüsünü güncelleyen metot olacak.



    }
}
