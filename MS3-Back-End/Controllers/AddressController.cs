﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.Address;
using MS3_Back_End.DTOs.ResponseDTOs.Address;
using MS3_Back_End.IService;

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



    }
}
