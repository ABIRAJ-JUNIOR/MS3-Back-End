using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using System.Collections;

namespace MS3_Back_End.Repository
{
    public class StudentRepository:IStudentRepository
    {
        private readonly AppDBContext _dbContext;

        public StudentRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Student>> GetAllStudents()
        {
            var students = await _dbContext.Students.Include(a => a.address).ToListAsync();
            return students;
        }

        public async Task<Student> GetStudentByNic(string nic)
        {
            var student = await _dbContext.Students.Include(a => a.address).SingleOrDefaultAsync(s => s.Nic.ToLower() == nic.ToLower());
            return student!;
        }

        public async Task<Student> AddStudent(Student student)
        {
            var studentDetail = await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return studentDetail.Entity;
        }
    }
}
