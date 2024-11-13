using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IAdminRepository
    {
        Task<Admin> AddAdmin(Admin admin);
        Task<Admin> GetAdminByNic(string nic);
        Task<ICollection<Admin>> GetAllAdmins();
        Task<Admin> UpdateAdmin(Admin admin);
    }
}
