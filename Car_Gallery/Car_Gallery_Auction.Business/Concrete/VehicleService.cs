using AutoMapper;
using Car_Gallery_Auction.Business.Abstract;
using Car_Gallery_Auction.Business.Dtos;
using Car_Gallery_Auction.DataBase.Context;
using Car_Gallery_Auction.DataBase.Domain;
using Car_Galllery_Auction.Core.Modals;
using Microsoft.EntityFrameworkCore;

namespace Car_Gallery_Auction.Business.Concrete
{
    public class VehicleService : IVehicleService
    {


        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApiResponse _apiResponse;
    

        public VehicleService(
            ApplicationDbContext context,
            IMapper mapper,
            ApiResponse apiResponse
        

        )
        {
        
            _context = context;
            _mapper = mapper;
            _apiResponse = apiResponse;
         
        }


        public async Task<ApiResponse> ChangeVehicleStatus(int vehicleId)
        {
            var result = await _context.Vehicles.FindAsync(vehicleId);

            if (result != null) // buraya giriyor ise demekki ilgili araç listede var.
            {

                result.IsActive = false;
                if (await _context.SaveChangesAsync() > 0)
                {
                    _apiResponse.isSuccess = true;
                    return _apiResponse;
                }
            }

            _apiResponse.isSuccess = false;

            return _apiResponse;

        }

        public async Task<ApiResponse> CreateVehicle(CreateVehicleDTO model)
        {
           if ( model != null )
            {
                var objDTO = _mapper.Map<Vehicle>( model );

                if ( objDTO != null )
                {

                     _context.Vehicles.Add( objDTO ); // ekleme işlemini yapan konum


                    if (await _context.SaveChangesAsync() > 0) // herhangi bir işlemi kaydetmiş ise
                    {

                        _apiResponse.isSuccess = true;
                        _apiResponse.Result = model;
                        _apiResponse.StatusCode = System.Net.HttpStatusCode.Created;

                        return _apiResponse;


                    }

                }

            }


           _apiResponse.isSuccess = false;
            _apiResponse.ErrorMessages.Add("Oooops! Something went wrong!");
            
            return _apiResponse;

        }

        public async Task<ApiResponse> DeleteVehicle(int vehicleId)
        {
         var result = await _context.Vehicles.FindAsync(vehicleId); // ID'ye göre tablodan geliyor


            if (result != null)
            {

                _context.Vehicles.Remove( result );

                if (await _context.SaveChangesAsync() > 0) // kayıt atılmış ise değer döner
                {
                    _apiResponse.isSuccess = true;
                    
                    return _apiResponse;
                }

            }

            _apiResponse.isSuccess = false;

            return _apiResponse;
        }

        public async Task<ApiResponse> GetVehicleById(int vehicleId)
        {
            var result = await _context.Vehicles.Include(x => x.Seller).FirstOrDefaultAsync(x => x.VehicleID == vehicleId);

            if (result != null)
            {

                _apiResponse.isSuccess= true;
                _apiResponse.Result= result;
                _apiResponse.StatusCode= System.Net.HttpStatusCode.OK;

                return _apiResponse;    

            }


            _apiResponse.isSuccess = false;
       
            return _apiResponse;
        }

        public async Task<ApiResponse> GetVehicles()
        {
            var vehicle = await _context.Vehicles.Include(x => x.Seller).ToListAsync();
            if (vehicle != null )
            {

                _apiResponse.isSuccess= true;
                _apiResponse.Result= vehicle;
                _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;

                return _apiResponse;

            }


            _apiResponse.isSuccess = false;
            _apiResponse.StatusCode = System.Net.HttpStatusCode.NotFound;

            return _apiResponse;


        }

        public async Task<ApiResponse> UpdateVehicleResponse(int vehicleId, UpdateVehicleDTO model)
        {
            var result = await _context.Vehicles.FindAsync(vehicleId);

            if (result != null) // buraya giriyor ise demekki ilgili araç listede var.
            {

                Vehicle objDTO = _mapper.Map(model,result);

                if (await _context.SaveChangesAsync() > 0)
                {
                   _apiResponse.isSuccess= true;
                    _apiResponse.Result= objDTO;

                    return _apiResponse;
                }
            }

            _apiResponse.isSuccess = false;

            return _apiResponse;

        }
    }
}
