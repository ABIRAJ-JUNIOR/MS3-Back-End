using MS3_Back_End.DTOs.Email;
using MS3_Back_End.DTOs.RequestDTOs.ContactUs;
using MS3_Back_End.DTOs.ResponseDTOs.ContactUs;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MS3_Back_End.Service
{
    public class ContactUsService : IContactUsService
    {
        private readonly IContactUsRepository _contactUsRepository;
        private readonly SendMailService _sendMailService;
        private readonly ILogger<ContactUsService> _logger;

        public ContactUsService(IContactUsRepository contactUsRepository, SendMailService sendMailService, ILogger<ContactUsService> logger)
        {
            _contactUsRepository = contactUsRepository;
            _sendMailService = sendMailService;
            _logger = logger;
        }

        public async Task<ContactUsResponseDTO> AddMessage(ContactUsRequestDTO requestDTO)
        {
            if (requestDTO == null)
            {
                throw new ArgumentNullException(nameof(requestDTO));
            }

            var message = new ContactUs
            {
                Name = requestDTO.Name,
                Email = requestDTO.Email,
                Message = requestDTO.Message,
                DateSubmited = DateTime.Now,
                IsRead = false
            };

            var data = await _contactUsRepository.AddMessage(message);

            var messageDetails = new SendMessageMailRequest
            {
                Name = data.Name,
                Email = data.Email,
                UserMessage = data.Message,
                EmailType = EmailTypes.Message,
            };

            await _sendMailService.MessageMail(messageDetails);

            var newContactUs = new ContactUsResponseDTO
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                Message = data.Message,
                Response = data.Response,
                DateSubmited = data.DateSubmited,
                IsRead = data.IsRead
            };

            _logger.LogInformation("Message added successfully with Id: {Id}", data.Id);

            return newContactUs;
        }

        public async Task<ICollection<ContactUsResponseDTO>> GetAllMessages()
        {
            var allMessages = await _contactUsRepository.GetAllMessages();
            if (allMessages == null || !allMessages.Any())
            {
                _logger.LogWarning("No messages found");
                throw new KeyNotFoundException("No messages found");
            }

            var contactUsResponse = allMessages.Select(message => new ContactUsResponseDTO
            {
                Id = message.Id,
                Name = message.Name,
                Email = message.Email,
                Message = message.Message,
                Response = message.Response,
                DateSubmited = message.DateSubmited,
                IsRead = message.IsRead
            }).ToList();

            return contactUsResponse;
        }

        public async Task<ContactUsResponseDTO> UpdateMessage(UpdateResponseRequestDTO request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var getData = await _contactUsRepository.GetMessageById(request.Id);
            if (getData == null)
            {
                _logger.LogWarning("Message not found for Id: {Id}", request.Id);
                throw new KeyNotFoundException("Message not found");
            }

            getData.Response = request.Response;
            getData.IsRead = true;

            var updatedData = await _contactUsRepository.UpdateMessage(getData);

            var messageDetails = new SendResponseMailRequest
            {
                Name = updatedData.Name,
                Email = updatedData.Email,
                AdminResponse = updatedData.Response,
                EmailType = EmailTypes.Response,
            };

            await _sendMailService.ResponseMail(messageDetails);

            var newUpdateMessage = new ContactUsResponseDTO
            {
                Id = updatedData.Id,
                Name = updatedData.Name,
                Email = updatedData.Email,
                Response = updatedData.Response,
                DateSubmited = updatedData.DateSubmited,
                Message = updatedData.Message,
                IsRead = updatedData.IsRead
            };

            _logger.LogInformation("Message updated successfully with Id: {Id}", updatedData.Id);

            return newUpdateMessage;
        }

        public async Task<ContactUsResponseDTO> DeleteMessage(Guid id)
        {
            var message = await _contactUsRepository.GetMessageById(id);
            if (message == null)
            {
                _logger.LogWarning("Message not found for Id: {Id}", id);
                throw new KeyNotFoundException("Message not found");
            }

            var data = await _contactUsRepository.DeleteMessage(message);

            var deletedData = new ContactUsResponseDTO
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                Message = data.Message,
                Response = data.Response,
                DateSubmited = data.DateSubmited,
                IsRead = data.IsRead
            };

            _logger.LogInformation("Message deleted successfully with Id: {Id}", data.Id);

            return deletedData;
        }
    }
}
