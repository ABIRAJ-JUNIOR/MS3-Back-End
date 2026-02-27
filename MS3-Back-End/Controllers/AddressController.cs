using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.Address;
using MS3_Back_End.DTOs.ResponseDTOs.Address;
using MS3_Back_End.IService;
using NLog;
using System.Runtime.InteropServices;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost("Add-Address")]
        public async Task<ActionResult<AddressResponseDTO>> AddAddress(AddressRequestDTO address)
        {
            try
            {
                var data = await _addressService.AddAddress(address);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding address");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update-Address/{id}")]
        public async Task<ActionResult<AddressResponseDTO>> UpdateAddress(Guid id, AddressUpdateRequestDTO updateAddress)
        {
            try
            {
                var data = await _addressService.UpdateAddress(id, updateAddress);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating address");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete-Address/{id}")]
        public async Task<ActionResult<AddressResponseDTO>> DeleteAddress(Guid id)
        {
            try
            {
                var data = await _addressService.DeleteAddress(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error deleting address");
                return BadRequest(ex.Message);
            }
        }
    }
}
