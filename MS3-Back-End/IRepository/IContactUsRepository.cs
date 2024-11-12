using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IContactUsRepository
    {
        Task<ContactUs> AddMessage(ContactUs contactUs);
    }
}
