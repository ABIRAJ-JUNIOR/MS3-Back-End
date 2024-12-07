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
        public async Task<ICollection<Enrollment>> SearchEnrollments(Guid SearchId)
        {
            var data = await _Db.Enrollments.Include(p => p.Payments).Where(x=>x.StudentId == SearchId).Where(e => e.IsActive == true).ToListAsync();
            return data;
        }

        public async Task<ICollection<Enrollment>> GetEnrollments()
        {
            var data = await _Db.Enrollments.Include(p => p.Payments).Where(e => e.IsActive == true).ToListAsync();
            return data;
        }
        public async Task<Enrollment> GetEnrollmentById(Guid EnrollmentId)
        {
            var data = await _Db.Enrollments.Include(p => p.Payments).SingleOrDefaultAsync(c => c.Id == EnrollmentId);
            return data!;
        }

        public async Task<Enrollment> UpdateEnrollment(Enrollment Enrollment)
        {
            var updatedData = _Db.Update(Enrollment);
            await _Db.SaveChangesAsync();
            return updatedData.Entity;
        }

        public async Task<string> DeleteEnrollment(Enrollment course)
        {
            _Db.Enrollments.Update(course);
            await _Db.SaveChangesAsync();
            return "Enrollment IsActivate Successfull";
        }
    }
}
