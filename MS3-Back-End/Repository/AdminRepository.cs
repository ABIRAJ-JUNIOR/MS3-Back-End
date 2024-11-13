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
    }
}
