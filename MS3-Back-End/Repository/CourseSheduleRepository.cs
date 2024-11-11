using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;

namespace MS3_Back_End.Repository
{
    public class CourseSheduleRepository
    {
        private readonly AppDBContext _Db;
        public CourseSheduleRepository(AppDBContext db)
        {
            _Db = db;

        }

      
    }
}
