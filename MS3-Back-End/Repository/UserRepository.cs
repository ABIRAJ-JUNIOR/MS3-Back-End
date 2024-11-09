using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.DTO.RequestDTOs;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _dbContext;

        public UserRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<User> SignUp(User user)
        {
            var userData = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return userData.Entity;
        }

        public async Task<User> GetUserByNic(string nic)
        {
            var userData = await _dbContext.Users.FirstOrDefaultAsync(u => u.Nic.ToLower() == nic.ToLower());
            return userData!;
        }

        public async Task<User> Signin(SignInRequestDTO signInRequest)
        {
            var userData = await _dbContext.Users.SingleOrDefaultAsync(u => u.Nic == signInRequest.Nic);
            return userData!;
        }

        public async Task<ICollection<User>> GetAllStudent()
        {
            var userList = await _dbContext.Users.Where(u => u.Role == Roles.Student).ToListAsync();
            return userList;
        }
    }
}
