using MS3_Back_End.DTOs.Email;
using MS3_Back_End.DTOs.RequestDTOs.ContactUs;
using MS3_Back_End.DTOs.ResponseDTOs.ContactUs;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MS3_Back_End.Service
{
    public class ContactUsService: IContactUsService
    {
        public readonly IContactUsRepository _contactUsRepository;
        public readonly SendMailService _sendMailService;

        public ContactUsService(IContactUsRepository contactUsRepository, SendMailService sendMailService)
        {
            _contactUsRepository = contactUsRepository;
            _sendMailService = sendMailService;
        }

        public async Task<ContactUsResponseDTO> AddMessage(ContactUsRequestDTO requestDTO)
        {
            var Message = new ContactUs
            {
                Name = requestDTO.Name,
                Email = requestDTO.Email,
                Message = requestDTO.Message,
                DateSubmited = DateTime.Now,
                IsRead = false
            };

            var data = await _contactUsRepository.AddMessage(Message);

            var messageDetails = new SendMessageMailRequest()
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
            return newContactUs;
        }

        public async Task<ICollection<ContactUsResponseDTO>> GetAllMessages()
        {
            var allMessages = await _contactUsRepository.GetAllMessages();
            if (allMessages == null)
            {
                throw new Exception("No messages");
            }
            
            var ContactUsResponse = allMessages.Select(message => new ContactUsResponseDTO()
            {
                Id = message.Id,
                Name = message.Name,
                Email = message.Email,
                Message = message.Message,
                Response = message.Response,
                DateSubmited = DateTime.Now,
                IsRead = message.IsRead
            }).ToList();

            return ContactUsResponse;
        }

        public async Task<ContactUsResponseDTO> UpdateMessage(UpdateResponseRequestDTO request)
        {
            var GetData = await _contactUsRepository.GetMessageById(request.Id);
            if(GetData == null)
            {
                throw new Exception("Not found");
            }

            GetData.Response = request.Response;
            GetData.IsRead = true;

            var UpdatedData = await _contactUsRepository.UpdateMessage(GetData);

            var messageDetails = new SendResponseMailRequest()
            {
                Name = UpdatedData.Name,
                Email = UpdatedData.Email,
                AdminResponse = UpdatedData.Response,
                EmailType = EmailTypes.Response,
            };

            await _sendMailService.ResponseMail(messageDetails);

            var newUpdateMessage = new ContactUsResponseDTO
            {
                Id = UpdatedData.Id,
                Name = UpdatedData.Name,
                Email = UpdatedData.Email,
                Response = UpdatedData.Response,
                DateSubmited = DateTime.Now,
                Message = UpdatedData.Message,
                IsRead = UpdatedData.IsRead
            };
            return newUpdateMessage;
        }

        public async Task<ContactUsResponseDTO> DeleteMessage(Guid id)
        {
            var message = await _contactUsRepository.GetMessageById(id);
            if(message == null)
            {
                throw new Exception("message not found");
            }

            var data = await _contactUsRepository.DeleteMessage(message);
            var deleteddata = new ContactUsResponseDTO
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                Message = data.Message,
                Response = data.Response,
                DateSubmited = data.DateSubmited,
                IsRead = data.IsRead
            };
            return deleteddata;
        }

    }
}
