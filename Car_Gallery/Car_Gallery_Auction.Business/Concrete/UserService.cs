using AutoMapper;
using Azure;
using Car_Gallery_Auction.Business.Abstract;
using Car_Gallery_Auction.Business.Dtos;
using Car_Gallery_Auction.DataBase.Context;
using Car_Gallery_Auction.DataBase.Enums;
using Car_Gallery_Auction.DataBase.Modals;
using Car_Galllery_Auction.Core.Modals;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Car_Gallery_Auction.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApiResponse _apiResponse;
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string secretkey;

        public UserService(
            ApplicationDbContext context,
            IMapper mapper,
            ApiResponse apiResponse,
            UserManager<ApplicationUser> usermanager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration _configuration
            
        )
        {
            _roleManager = roleManager;
            secretkey = _configuration.GetValue<string>("SecretKey:jwtKey");
            _context = context;
            _mapper = mapper;
            _apiResponse = apiResponse;
            _usermanager = usermanager;

        }
        public async Task<ApiResponse> Login(LoginRequestDto model)
        {

            ApplicationUser UserFromDb = _context.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());


            if (UserFromDb != null)
            {  // kullanıcı db de varsa paraloasının doğru olup olmadığının sorgulanması gerekiyor.

                bool isValid = await _usermanager.CheckPasswordAsync(UserFromDb, model.Password);

                if (!isValid)
                {

                    _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _apiResponse.ErrorMessages.Add("Your entry iformation is not correct");
                    _apiResponse.isSuccess = false;

                    return _apiResponse;

                }


                var role = await _usermanager.GetRolesAsync(UserFromDb);
                JwtSecurityTokenHandler tokenHandler = new();

                byte[] key = Encoding.ASCII.GetBytes(secretkey);

                SecurityTokenDescriptor tokenDescriptor = new()
                {

                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, UserFromDb.Id),
                        new Claim(ClaimTypes.Email, UserFromDb.Email),
                        new Claim(ClaimTypes.Role, role.FirstOrDefault()), // tek role üzerinden çalıştığımız için bu mantıklı oluyor.
                        new Claim("fullname", UserFromDb.FullName),
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),  // tokenının ne kadar geçerli olacağını belirliyoruz burada
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) // hangi şifreleme algoritmasının kullanılacağını veriyoruz burada.
                };


                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor); // tokenın oluşturulduğunu konum

                LoginResponseModal _responseModel = new()
                {
                    Email = UserFromDb.Email,
                    Token = tokenHandler.WriteToken(token),
                };

                _apiResponse.Result = _responseModel;
                _apiResponse.isSuccess = true;
                _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;

                return _apiResponse;



            }

            _apiResponse.isSuccess = false;
            _apiResponse.ErrorMessages.Add("Oooops! Something went wrong!");

            return _apiResponse;


        }

        public async Task<ApiResponse> Register(RegisterRequestDto model)
        {
            var userFromDb = _context.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == model.UserName.ToLower());


            if (userFromDb != null)
            {
                _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _apiResponse.isSuccess = false;
                _apiResponse.ErrorMessages.Add("Username already exist");

                return _apiResponse;
            }

            ApplicationUser newUser = new ApplicationUser()
            {
                FullName = model.FullName,
                UserName = model.UserName,
                NormalizedEmail = model.UserName.ToUpper(),
                Email = model.UserName
            
              
            };


           // var newUser = _mapper.Map<ApplicationUser>(model); // birbirleri arasındaki maplemeyi yapıyor burada 

            var result = await _usermanager.CreateAsync(newUser, model.Password);
            //var message = string.Join(", ", result.Errors.Select(x => "Code " + x.Code + " Description" + x.Description));
            //throw new Exception(message.ToString());
            if (result.Succeeded)
            {
                var isTrue = _roleManager.RoleExistsAsync(UserTypes.Administrator.ToString()).GetAwaiter().GetResult();


                if (!_roleManager.RoleExistsAsync(UserTypes.Administrator.ToString()).GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserTypes.Administrator.ToString())); // yeni kayıt yapılacak kullanıcının role bilgisi eğerki role tablosunda yoksa burada role tablosuna bir create atma işlemi yapıyoruz.
                    await _roleManager.CreateAsync(new IdentityRole(UserTypes.Seller.ToString()));
                    await _roleManager.CreateAsync(new IdentityRole(UserTypes.NormalUser.ToString()));
                }


                if (model.UserType.ToString().ToLower() == UserTypes.Administrator.ToString().ToLower()) // kayıt için gelen kullanıcının rolu admin ile eşleşiyor ise bu kullanıcıya admin rolu atamasını yapacağız.
                {

                    await _usermanager.AddToRoleAsync(newUser, UserTypes.Administrator.ToString());
                }


                if (model.UserType.ToString().ToLower() == UserTypes.Seller.ToString().ToLower()) // kayıt için gelen kullanıcının rolu seller ile eşleşiyor ise bu kullanıcıya seller rolu atamasını yapacağız.
                {

                    await _usermanager.AddToRoleAsync(newUser, UserTypes.Seller.ToString());
                }
                else if(model.UserType.ToString().ToLower() == UserTypes.NormalUser.ToString().ToLower())
                { 

                    await _usermanager.AddToRoleAsync(newUser, UserTypes.NormalUser.ToString());
                }




                _apiResponse.StatusCode = System.Net.HttpStatusCode.Created;
                _apiResponse.isSuccess = true;


                return _apiResponse;

            }




            foreach (var error in result.Errors)
            {
                _apiResponse.ErrorMessages.Add(string.Join(", ", result.Errors.Select(x => "Code " + x.Code + " Description" + x.Description)));
            }


            return _apiResponse;
        }
    }
}
