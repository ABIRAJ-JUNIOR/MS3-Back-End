using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class EnrollmentRepository:IEnrollmentRepository
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
            var data = await _Db.Enrollments.Include(p => p.Payments).Where(x=>x.StudentId == SearchId).ToListAsync();
            return data;
        }

        public async Task<List<Enrollment>> GetEnrollments()
        {
            var data = await _Db.Enrollments.Include(p => p.Payments).ToListAsync();
            return data;
        }
        public async Task<Enrollment> GetEnrollmentById(Guid EnrollmentId)
        {
            var data = await _Db.Enrollments.Include(p => p.Payments).SingleOrDefaultAsync(c => c.Id == EnrollmentId);
            return data;
        }

        public async Task<string> DeleteEnrollment(Enrollment course)
        {
            _Db.Enrollments.Update(course);
            await _Db.SaveChangesAsync();
            return "Enrollment IsActivate Successfull";
        }
    }
}
