using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DTOs.RequestDTOs.ContactUs;
using MS3_Back_End.Entities;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsService _contactUsService;

        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        [HttpPost("Add-Message")]
        public async Task<IActionResult> AddMessage(ContactUsRequestDTO contactUsRequestDTO)
        {
            try
            {
                var message = await _contactUsService.AddMessage(contactUsRequestDTO);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get-All-Messages")]

        public async Task<IActionResult> GetAllMessages()
        {
            try
            {
                var result = await _contactUsService.GetAllMessages();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update-Message")]

        public async Task<IActionResult> UpdateMessage(UpdateResponseRequestDTO request)
        {
            try
            {
                var updateMessage = await _contactUsService.UpdateMessage(request);
                return Ok(updateMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
