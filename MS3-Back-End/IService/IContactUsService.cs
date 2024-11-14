using MS3_Back_End.DTOs.RequestDTOs.ContactUs;
using MS3_Back_End.DTOs.ResponseDTOs.ContactUs;

namespace MS3_Back_End.IService
{
    public interface IContactUsService
    {
        Task<ContactUsResponseDTO> AddMessage(ContactUsRequestDTO requestDTO);
        Task<List<ContactUsResponseDTO>> GetAllMessages();
        Task<ContactUsResponseDTO> GetMessageById(Guid Id);
        Task<ContactUsResponseDTO> UpdateMessage(ContactUsRequestDTO contactUsRequestDTO);
    }
}
