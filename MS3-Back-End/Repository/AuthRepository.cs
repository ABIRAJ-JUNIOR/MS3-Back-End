using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDBContext _dbContext;

        public AuthRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Student> SignUp(Student student)
        {
            var studentData = await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return studentData.Entity;
        }

        public async Task<User> AddUser(User user)
        {
            var userData = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return userData.Entity;
        }

        public async Task<Role> GetRoleByName(string name)
        {
            var roleData = await _dbContext.Roles.SingleOrDefaultAsync(r => r.Name == name);
            return roleData!;
        }

        public async Task<UserRole> AddUserRole(UserRole userRole)
        {
            var userRoleData = await _dbContext.UserRoles.AddAsync(userRole);
            await _dbContext.SaveChangesAsync();
            return userRoleData.Entity;
        }

        public async Task<Student> GetStudentByNic(string nic)
        {
            var studentData = await _dbContext.Students.SingleOrDefaultAsync(s => s.Nic.ToLower() == nic.ToLower());
            return studentData!;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var userData = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
            return userData!;
        }

        public async Task<Role> GetRoleById(Guid id)
        {
            var roleData = await _dbContext.Roles.SingleOrDefaultAsync(r => r.Id == id);
            return roleData!;
        }


        public async Task<UserRole> GetUserRoleByUserId(Guid userId)
        {
            var userRoleData = await _dbContext.UserRoles.SingleOrDefaultAsync(ur => ur.UserId == userId);
            return userRoleData!;
        }


        public async Task<Student> GetStudentById(Guid id)
        {
            var studentData = await _dbContext.Students.SingleOrDefaultAsync(s => s.Id == id);
            return studentData!;
        }

        public async Task<Admin> GetAdminById(Guid id)
        {
            var adminData = await _dbContext.Admins.SingleOrDefaultAsync(s => s.Id == id);
            return adminData!;
        }
    }
}
