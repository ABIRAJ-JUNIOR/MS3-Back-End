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
            var adminData = await _dbContext.Admins.Include(al => al.AuditLogs).SingleOrDefaultAsync(a => a.Nic.ToLower() == nic.ToLower());
            return adminData!;
        }

        public async Task<Admin> GetAdminById(Guid id)
        {
            var adminData = await _dbContext.Admins.Include(al => al.AuditLogs).SingleOrDefaultAsync(a => a.Id == id);
            return adminData!;
        }


        public async Task<ICollection<Admin>> GetAllAdmins()
        {
            var adminsList = await _dbContext.Admins.Include(al => al.AuditLogs).ToListAsync();
            return adminsList;
        }

        public async Task<Admin> UpdateAdmin(Admin admin)
        {
            var updatedData = _dbContext.Admins.Update(admin);
            await _dbContext.SaveChangesAsync();
            return updatedData.Entity;
        }

        public async Task<User> UpdateUser(User user)
        {
            var updatedData = _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return updatedData.Entity;
        }

        public async Task<User> GetUserById(Guid id)
        {
            var userData = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
            return userData!;
        }

        public async Task<ICollection<Admin>> GetPaginatedAdmin(int pageNumber, int pageSize)
        {
            return await _dbContext.Admins.Include(a => a.AuditLogs)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
