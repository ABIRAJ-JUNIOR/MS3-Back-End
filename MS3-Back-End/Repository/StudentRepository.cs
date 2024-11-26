using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.ResponseDTOs.Address;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
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
            try
            {
                var data = await _Db.Students.AddAsync(StudentReq);
                await _Db.SaveChangesAsync();
                return data.Entity;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error occurred while adding the student to the database.", ex);
            }
        }

        public async Task<ICollection<Student>> SearchStudent(string SearchText)
        {
            var data = await _Db.Students .Where(n => n.FirstName.Contains(SearchText) || 
               n.LastName!.Contains(SearchText) ||
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
            var data = await _Db.Students
                .Include(a => a.Address)
                .Include(e => e.Enrollments!)
                    .ThenInclude(p => p.Payments)
                .Include(e => e.Enrollments!)
                    .ThenInclude(enrollment => enrollment.CourseSchedule).ThenInclude(c => c!.Course)
                .Include(a => a.StudentAssessments!)
                    .ThenInclude(a => a.Assessment)
                .SingleOrDefaultAsync(c => c.Id == StudentId && c.IsActive == true);
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

        public async Task<ICollection<StudentWithUserResponseDTO>> GetPaginatedStudent(int pageNumber, int pageSize)
        {
            var students = await (from student in _Db.Students
                                  join address in _Db.Addresses on student.Id equals address.StudentId
                                  join user in _Db.Users on student.Id equals user.Id
                                  where student.IsActive != false
                                  select new StudentWithUserResponseDTO
                                  {
                                      Id = student.Id,
                                      Nic = student.Nic,
                                      FirstName = student.FirstName,
                                      LastName = student.LastName,
                                      DateOfBirth = student.DateOfBirth,
                                      Gender = ((Gender)student.Gender).ToString(),
                                      Phone = student.Phone,
                                      Email = user.Email,
                                      ImageUrl = student.ImageUrl!,
                                      CteatedDate = student.CteatedDate,
                                      UpdatedDate = student.UpdatedDate,
                                      Address = student.Address != null ? new AddressResponseDTO()
                                      {
                                          AddressLine1 = address.AddressLine1,
                                          AddressLine2 = address.AddressLine2,
                                          City = address.City,
                                          PostalCode = address.PostalCode,
                                          Country = address.Country,
                                          StudentId = address.Id
                                      } : null,
                                  })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return students;
        }
    }
}
