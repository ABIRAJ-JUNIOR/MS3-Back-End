using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IUserRepository
    {
        Task<User> SignUp(User user);
    }
}
