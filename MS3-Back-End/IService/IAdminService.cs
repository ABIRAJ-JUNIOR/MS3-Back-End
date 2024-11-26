using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Image;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.__Password__;
using MS3_Back_End.DTOs.RequestDTOs.Admin;
using MS3_Back_End.DTOs.ResponseDTOs.Admin;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IService
{
    public interface IAdminService
    {
        Task<AdminResponseDTO> AddAdmin(AdminRequestDTO request);
        Task<AdminResponseDTO> GetAdminById(Guid id);
        Task<ICollection<AdminResponseDTO>> GetAllAdmins();
        Task<AdminResponseDTO> UpdateAdminFullDetails(Guid id, AdminFullUpdateDTO request);
        Task<AdminResponseDTO> UpdateAdminPersonalDetails(Guid id, AdminUpdateRequestDTO request);
        Task<string> UpdateEmail(UpdateEmailRequestDTO request);
        Task<string> UploadImage(Guid adminId,IFormFile? ImageFile);
        Task<string> UpdatePassword(UpdatePasswordRequestDTO request);
        Task<PaginationResponseDTO<AdminWithRoleDTO>> GetPaginatedAdmin(int pageNumber, int pageSize);
        Task<AdminResponseDTO> DeleteAdmin(Guid Id);
    }
}
