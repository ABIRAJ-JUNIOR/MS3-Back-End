using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class StudentRepository:IStudentRepository
    {
        private readonly AppDBContext _dbContext;

        public StudentRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
