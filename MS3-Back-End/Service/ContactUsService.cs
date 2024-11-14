using MS3_Back_End.DTOs.RequestDTOs.ContactUs;
using MS3_Back_End.DTOs.ResponseDTOs.ContactUs;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class ContactUsService: IContactUsService
    {
        public readonly IContactUsRepository _contactUsRepository;

        public ContactUsService(IContactUsRepository contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
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

            var newContactUs = new ContactUsResponseDTO
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                Message = data.Message,
                DateSubmited = data.DateSubmited,
                IsRead = data.IsRead
            };
            return newContactUs;
        }

        public async Task<List<ContactUsResponseDTO>> GetAllMessages()
        {
            var allMessages = await _contactUsRepository.GetAllMessages();
            if (allMessages == null)
            {
                throw new Exception("No messages");
            }
            var ContactUsResponse = new List<ContactUsResponseDTO>();
            foreach (var message in allMessages)
            {
                var obj = new ContactUsResponseDTO
                {
                    Id = message.Id,
                    Name = message.Name,
                    Email = message.Email,
                    Message = message.Message,
                };
                ContactUsResponse.Add(obj);
            }
            return ContactUsResponse;
        }

        public async Task<ContactUsResponseDTO> GetMessageById(Guid Id)
        {
            var data = await _contactUsRepository.GetMessageById(Id);
            if (data == null)
            {
                throw new Exception("Messages not found or Invalid Id");
            }
            var contactResponse = new ContactUsResponseDTO
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                Message = data.Message,
            };
            return contactResponse;
        }

        public async Task<ContactUsResponseDTO> UpdateMessage(ContactUsRequestDTO contactUsRequestDTO)
        {
            var GetData = await _contactUsRepository.GetMessageById(contactUsRequestDTO.Id);
            GetData.Name = contactUsRequestDTO.Name;
            GetData.Email = contactUsRequestDTO.Email;
            GetData.Message = contactUsRequestDTO.Message;

            var UpdatedData = await _contactUsRepository.UpdateMessage(GetData);

            var newUpdateMessage = new ContactUsResponseDTO
            {
                Id = contactUsRequestDTO.Id,
                Name = contactUsRequestDTO.Name,
                Email = contactUsRequestDTO.Email,
                Message = contactUsRequestDTO.Message,
            };
            return newUpdateMessage;
        }

    }
}
