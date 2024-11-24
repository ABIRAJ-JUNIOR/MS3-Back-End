using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Image;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.__Password__;
using MS3_Back_End.DTOs.RequestDTOs.Admin;
using MS3_Back_End.DTOs.ResponseDTOs.Admin;

namespace MS3_Back_End.IService
{
    public interface IAdminService
    {
        Task<AdminResponseDTO> AddAdmin(AdminRequestDTO request);
        Task<AdminResponseDTO> GetAdminById(Guid id);
        Task<ICollection<AdminResponseDTO>> GetAllAdmins();
        Task<AdminResponseDTO> UpdateAdmin(Guid id, AdminUpdateRequestDTO request);
        Task<string> UpdateEmail(UpdateEmailRequestDTO request);
        Task<string> UploadImage(Guid adminId, [FromForm] IFormFile ImageFile);
        Task<string> UpdatePassword(UpdatePasswordRequestDTO request);
        Task<PaginationResponseDTO<AdminResponseDTO>> GetPaginatedAdmin(int pageNumber, int pageSize);
    }
}
