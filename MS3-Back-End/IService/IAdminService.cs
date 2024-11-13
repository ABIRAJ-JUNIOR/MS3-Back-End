using MS3_Back_End.DTOs.RequestDTOs.Admin;
using MS3_Back_End.DTOs.ResponseDTOs.Admin;

namespace MS3_Back_End.IService
{
    public interface IAdminService
    {
        Task<AdminResponseDTO> AddAdmin(AdminRequestDTO request);
        Task<ICollection<AdminResponseDTO>> GetAllAdmins();
        Task<AdminResponseDTO> UpdateAdmin(Guid id, AdminUpdateRequestDTO request);
        Task<string> UpdateEmail(Guid studentId, string email, string password);
    }
}
