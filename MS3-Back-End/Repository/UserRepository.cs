using MS3_Back_End.DBContext;
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
    }
}
