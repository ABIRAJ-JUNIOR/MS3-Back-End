using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DTOs.RequestDTOs.ContactUs;
using MS3_Back_End.DTOs.ResponseDTOs.ContactUs;
using MS3_Back_End.Entities;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsService _contactUsService;
        private readonly ILogger<ContactUsController> _logger;

        public ContactUsController(IContactUsService contactUsService, ILogger<ContactUsController> logger)
        {
            _contactUsService = contactUsService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ContactUsResponseDTO>> AddMessage(ContactUsRequestDTO contactUsRequestDTO)
        {
            if (contactUsRequestDTO == null)
            {
                return BadRequest("Contact us data is required.");
            }

            try
            {
                var message = await _contactUsService.AddMessage(contactUsRequestDTO);
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding message");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactUsResponseDTO>>> GetAllMessages()
        {
            try
            {
                var result = await _contactUsService.GetAllMessages();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all messages");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ContactUsResponseDTO>> UpdateMessage(UpdateResponseRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Update data is required.");
            }

            try
            {
                var updateMessage = await _contactUsService.UpdateMessage(request);
                return Ok(updateMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating message");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ContactUsResponseDTO>> DeleteMessage(Guid id)
        {
            try
            {
                var deletedData = await _contactUsService.DeleteMessage(id);
                return Ok(deletedData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting message with id {id}");
                return BadRequest(ex.Message);
            }
        }
    }
}
