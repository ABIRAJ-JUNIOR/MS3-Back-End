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

        //SignUP 
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

        public async Task<Role> GetRole()
        {
            var roleData = await _dbContext.Roles.SingleOrDefaultAsync(r => r.Name == "Student");
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


        //SignIn
        public async Task<User> SignIn(string email)
        {
            var userData = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
            return userData!;
        }
    }
}
