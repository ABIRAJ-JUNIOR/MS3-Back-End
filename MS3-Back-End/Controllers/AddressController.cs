using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.Address;
using MS3_Back_End.DTOs.ResponseDTOs.Address;
using MS3_Back_End.IService;
using System.Runtime.InteropServices;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost("Add-Addrees")]
        public async Task<IActionResult> AddAddress(AddressRequestDTO address)
        {
            try
            {

                var data = await _addressService.AddAddress(address);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update-Address/{id}")]
        public async  Task<IActionResult> UpdateAddress(Guid id, AddressUpdateRequestDTO Updateaddress)
        {
            try
            {
                var data = await _addressService.UpdateAddress(id,Updateaddress);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("Delete-Address/{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            try
            {
                var data = await _addressService.DeleteAddress(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
