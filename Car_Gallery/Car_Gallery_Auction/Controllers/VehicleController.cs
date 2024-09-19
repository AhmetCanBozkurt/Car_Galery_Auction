using Car_Gallery_Auction.Business.Abstract;
using Car_Gallery_Auction.Business.Dtos;
using Car_Gallery_Auction.DataBase.Domain;
using Car_Galllery_Auction.Core.Modals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Gallery_Auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {

        private readonly IVehicleService _vehicleService;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public VehicleController(IVehicleService vehicleService, IWebHostEnvironment webHostEnviroment)
        {
            _vehicleService = vehicleService;
            _webHostEnviroment = webHostEnviroment;

        }


        [HttpPost("CreateVehicle")]
        public async Task<IActionResult> AddVehicle([FromForm]CreateVehicleDTO model)
        {

            if (ModelState.IsValid)
            {
                if (model.File ==null || model.File.Length ==0)
                {
                    return BadRequest();
                }


                string uploadsFolder = Path.Combine(_webHostEnviroment.ContentRootPath,"Images"); // apinin içerisinden Images Klasörünü combineliyor

                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.File.FileName)}";
                string filePath = Path.Combine(uploadsFolder,fileName);


              
                model.Image = fileName;

                // görselin isminide vermiş oluyoruz.

                var result = await _vehicleService.CreateVehicle(model);

                if (result.isSuccess)
                {

                    using (var fileStream = new FileStream(filePath, FileMode.Create)) // başarılı olarak geldikten sonra görseli create ediyoruz.
                    {
                        await model.File.CopyToAsync(fileStream);
                    }
                    return  Ok(result);
                }
            }

            return BadRequest();


        }
        [HttpGet("GetVehicles")]
        public async Task<IActionResult> GetAllVehicle()
        {

            var vehicles = await _vehicleService.GetVehicles();

            return Ok(vehicles);

         }

        [HttpPut("updateVehicle")]
        public async Task<IActionResult> UpdateVehicle(int vehicleId, [FromForm] UpdateVehicleDTO model)
        {

            if (ModelState.IsValid)
            {
                var result = await _vehicleService.UpdateVehicleResponse(vehicleId, model);

                if (result.isSuccess)
                {
                    return Ok(result);
                }
            }

            return BadRequest();

        }
 // silme işlemini yapabilecek kişi giriş yapmış ve admin olan bir kullanıcı yapabilir.

        [Authorize(Roles = "Administrator")]
        [HttpDelete("Remove/Vehicle/{vehicleId}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int vehicleId)
        {

            var result = await _vehicleService.DeleteVehicle(vehicleId);

            if (result.isSuccess)
            {
               
                    return Ok(result);
              
            }

            return BadRequest();

        }

        [HttpGet("TestAuthentication")]
        public IActionResult TestAuthentication()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(new { message = "Authenticated", user = User.Identity.Name });
            }
            return Unauthorized();
        }

        [HttpGet("{vehicleId}")]
        public async Task<IActionResult> GetVehicleByID([FromRoute] int vehicleId)
        {

            var result = await _vehicleService.GetVehicleById(vehicleId);

            if (result.isSuccess)
            {

                return Ok(result);

            }

            return BadRequest();

        }

        [HttpPut("{vehicleId}")]
        public async Task<IActionResult> ChangeVehicleStatus([FromRoute] int vehicleId)
        {

            if (ModelState.IsValid)
            {
                var result = await _vehicleService.ChangeVehicleStatus(vehicleId);

                if (result.isSuccess)
                {

                    return Ok(result);

                }
            }


            return BadRequest();

        }

  


    }
}
