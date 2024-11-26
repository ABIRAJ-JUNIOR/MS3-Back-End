using MS3_Back_End.DTOs.ResponseDTOs.Admin;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IAdminRepository
    {
        Task<Admin> AddAdmin(Admin admin);
        Task<Admin> GetAdminByNic(string nic);
        Task<Admin> GetAdminById(Guid id);
        Task<ICollection<Admin>> GetAllAdmins();
        Task<Admin> UpdateAdmin(Admin admin);
        Task<ICollection<AdminWithRoleDTO>> GetPaginatedAdmin(int pageNumber, int pageSize);
        Task<Admin> DeleteAdmin(Admin admin);
    }
}
