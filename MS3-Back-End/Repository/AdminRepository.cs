using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDBContext _dbContext;

        public AdminRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Admin> AddAdmin(Admin admin)
        {
            var adminData = await _dbContext.Admins.AddAsync(admin);
            await _dbContext.SaveChangesAsync();
            return adminData.Entity;
        }

        public async Task<Admin> GetAdminByNic(string nic)
        {
            var adminData = await _dbContext.Admins.SingleOrDefaultAsync(a => a.Nic.ToLower() == nic.ToLower());
            return adminData!;
        }

        public async Task<Admin> GetAdminById(Guid id)
        {
            var adminData = await _dbContext.Admins.SingleOrDefaultAsync(a => a.Id == id);
            return adminData!;
        }


        public async Task<ICollection<Admin>> GetAllAdmins()
        {
            var adminsList = await _dbContext.Admins.ToListAsync();
            return adminsList;
        }

        public async Task<Admin> UpdateAdmin(Admin admin)
        {
            var updatedData = _dbContext.Admins.Update(admin);
            await _dbContext.SaveChangesAsync();
            return updatedData.Entity;
        }

    }
}
