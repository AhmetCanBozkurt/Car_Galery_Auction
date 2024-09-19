using AutoMapper;
using Car_Gallery_Auction.Business.Dtos;
using Car_Gallery_Auction.DataBase.Domain;
using Car_Gallery_Auction.DataBase.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Gallery_Auction.Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateVehicleDTO,Vehicle>().ReverseMap(); // çift taraflı mapleme yapabileceklerini söylüyoruz burada.

            CreateMap<UpdateVehicleDTO, Vehicle>().ReverseMap();
        }
    }
}
