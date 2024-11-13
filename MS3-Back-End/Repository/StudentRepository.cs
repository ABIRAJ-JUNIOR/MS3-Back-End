using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
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
            var student = await _Db.Students.SingleOrDefaultAsync(s=>s.Nic== StudentReq.Nic);
            if (student == null)
            {
                var data = await _Db.Students.AddAsync(StudentReq);
                await _Db.SaveChangesAsync();
                return data.Entity;
            }
            else
            {
                throw new Exception("Student Already Registered");
            }

        }


        public async Task<List<Student>> SearchStudent(string SearchText)
        {
            var data = await _Db.Students.Where(n => n.FirstName.Contains(SearchText) || n.LastName.Contains(SearchText) || n.Nic.Contains(SearchText).ToListAsync();
            return data;
        }




    }
}
