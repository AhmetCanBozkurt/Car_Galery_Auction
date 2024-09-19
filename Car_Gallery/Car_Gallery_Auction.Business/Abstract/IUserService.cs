using Car_Gallery_Auction.Business.Dtos;
using Car_Galllery_Auction.Core.Modals;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Gallery_Auction.Business.Abstract
{
    public interface IUserService
    {

        Task<ApiResponse> Register(RegisterRequestDto model);

        Task<ApiResponse> Login(LoginRequestDto model);
    }
}
