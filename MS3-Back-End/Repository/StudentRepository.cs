using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDBContext _Db;
        public StudentRepository(AppDBContext Db)
        {
            _Db = Db;
        }
        public async Task<Student> AddStudent(Student StudentReq)
        {
                var data = await _Db.Students.AddAsync(StudentReq);
                await _Db.SaveChangesAsync();
                return data.Entity;
        }

        public async Task<ICollection<Student>> SearchStudent(string SearchText)
        {
            var data = await _Db.Students .Where(n => n.FirstName.Contains(SearchText) || 
               n.LastName.Contains(SearchText) ||
               n.Nic.Contains(SearchText)).Include(a => a.Address).ToListAsync();
            return data;
        }

        public async Task<ICollection<Student>> GetAllStudente()
        {
            var data = await _Db.Students.Where(c => c.IsActive == true).Include(a => a.Address).ToListAsync();
            return data;
        }
       
        public async Task<Student> GetStudentById(Guid StudentId)
        {
            var data = await _Db.Students.Include(a => a.Address).Include(e => e.Enrollments).Include(a => a.Assessments).SingleOrDefaultAsync(c => c.Id == StudentId && c.IsActive == true);
            return data!;
        }

        public async Task<Student> UpdateStudent(Student Students)
        {
            var data = _Db.Students.Update(Students);
            await _Db.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<string> DeleteStudent(Student Student)
        {
            _Db.Students.Update(Student);
            await _Db.SaveChangesAsync();
            return "Student Deleted Successfull";
        }

        public async Task<ICollection<Student>> GetPaginatedStudent(int pageNumber, int pageSize)
        {
            return await _Db.Students.Include(a => a.Address)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
