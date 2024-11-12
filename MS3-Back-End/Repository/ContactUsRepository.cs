using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class ContactUsRepository: IContactUsRepository
    {
        public readonly AppDBContext _dbContext;

        public ContactUsRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

         public async Task<ContactUs> AddMessage(ContactUs contactUs)
         {
            var message = await _dbContext.ContactUs.AddAsync(contactUs);
            await _dbContext.SaveChangesAsync();
            return message.Entity;
         }

        public async Task<List<ContactUs>> GetAllMessages()
        {
            var getMessage = await _dbContext.ContactUs.ToListAsync();
            return getMessage;
        }

        public async Task<ContactUs> GetMessageById(Guid Id)
        {
            var getMessageById = await _dbContext.ContactUs.SingleOrDefaultAsync(C => C.Id == Id);
            return getMessageById;
        }

    }
}
