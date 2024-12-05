using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;

namespace MS3_Back_End.Repository
{
    public class SendMailRepository
    {
        private readonly AppDBContext _dbContext;

        public SendMailRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EmailTemplate> GetTemplate(EmailTypes emailTypes)
        {
            var template = await _dbContext.EmailTemplates.Where(x => x.emailTypes == emailTypes).FirstOrDefaultAsync();
            return template!;
        }
    }
}
