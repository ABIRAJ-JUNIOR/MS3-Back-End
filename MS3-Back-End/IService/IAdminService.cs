using MS3_Back_End.DTOs.Image;
using MS3_Back_End.DTOs.RequestDTOs.__Password__;
using MS3_Back_End.DTOs.RequestDTOs.Admin;
using MS3_Back_End.DTOs.ResponseDTOs.Admin;

namespace MS3_Back_End.IService
{
    public interface IAdminService
    {
        Task<AdminResponseDTO> AddAdmin(AdminRequestDTO request);
        Task<ICollection<AdminResponseDTO>> GetAllAdmins();
        Task<AdminResponseDTO> UpdateAdmin(Guid id, AdminUpdateRequestDTO request);
        Task<string> UpdateEmail(UpdateEmailRequestDTO request);
        Task<string> UploadImage(Guid adminId, ImageRequestDTO request);
        Task<string> UpdatePassword(UpdatePasswordRequestDTO request);
    }
}
