using Microsoft.AspNetCore.Mvc;
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
        Task<AdminAllDataResponseDTO> GetAdminFullDetailsById(Guid id);
        Task<ICollection<AdminResponseDTO>> GetAllAdmins();
        Task<AdminResponseDTO> UpdateAdminFullDetails(Guid id, AdminFullUpdateDTO request);
        Task<string> UploadImage(Guid adminId, IFormFile? ImageFile, bool isCoverImage);
        Task<PaginationResponseDTO<AdminWithRoleDTO>> GetPaginatedAdmin(int pageNumber, int pageSize);
        Task<AdminResponseDTO> DeleteAdmin(Guid Id);
        Task<string> UpdateAdminProfile(Guid ID, AdminProfileUpdateDTO request);

    }
}
