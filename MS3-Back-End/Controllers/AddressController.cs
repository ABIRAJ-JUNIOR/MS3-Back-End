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
        [HttpGet("GetAddressbyStudentID")]
        public async Task<IActionResult> GetAddressbyStuID(Guid stuID) 
        {
            try
            {
                var data = await _addressService.GetAddressbyStuID(stuID);
                return Ok(data);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllAddresses")]
        public async Task<IActionResult> GetAllAddress()
        {
            try
            {
                var data = await _addressService.GetAllAddress();
                return Ok(data);
            }
            catch (Exception ex) 
            {
               return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Delete-Address")]
        public async Task<IActionResult> DeleteAddress(Guid stuid)
        {
            try
            {
                var data = await _addressService.DeleteAddress(stuid);
                return Ok(data);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Update-Address")]
        public async  Task<IActionResult> UpdateAddress(AddressUpdateRequestDTO addressUpdate,Guid stuID)
        {
            try
            {
                var data = await _addressService.UpdateAddress(addressUpdate, stuID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> SearchbyCity(string searchText)
        {
           var data= await _addressService.SearchbyCity(searchText);    
            return Ok(data);
        }




    }
}
