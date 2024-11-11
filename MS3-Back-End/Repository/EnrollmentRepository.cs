using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;

namespace MS3_Back_End.Repository
{
    public class EnrollmentRepository
    {
        private readonly AppDBContext _Db;
        public EnrollmentRepository(AppDBContext db)
        {
            _Db = db;

        }

        public async Task<Enrollment> AddEnrollment(Enrollment Enrollment)
        {
                var data = await _Db.Enrollments.AddAsync(Enrollment);
                await _Db.SaveChangesAsync();
                return data.Entity;
        }
        public async Task<List<Enrollment>> SearchEnrollments(Guid SearchId)
        {
            var data = await _Db.Enrollments.Where(x=>x.StudentId == SearchId).ToListAsync();
            return data;
        }
        



    }
}
