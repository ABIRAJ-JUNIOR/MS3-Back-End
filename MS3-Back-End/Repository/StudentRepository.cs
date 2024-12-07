using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.ResponseDTOs.Address;
using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Enrollment;
using MS3_Back_End.DTOs.ResponseDTOs.Notification;
using MS3_Back_End.DTOs.ResponseDTOs.Payment;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
using MS3_Back_End.DTOs.ResponseDTOs.StudentAssessment;
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

        public async Task<Student> GetStudentById(Guid id)
        {
            var studentData = await _Db.Students.Include(a => a.Address).SingleOrDefaultAsync(s => s.Id == id);
            return studentData!;
        }
       
        public async Task<StudentFullDetailsResponseDTO> GetStudentFullDetailsById(Guid StudentId)
        {

            var data = await (from student in _Db.Students
                              join address in _Db.Addresses on student.Id equals address.StudentId into addressGroup
                              from address in addressGroup.DefaultIfEmpty()

                              join user in _Db.Users on student.Id equals user.Id into userGroup
                              from user in userGroup.DefaultIfEmpty()

                              where student.Id == StudentId && student.IsActive == true

                              select new StudentFullDetailsResponseDTO()
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
                                      AddressLine1 = student.Address!.AddressLine1,
                                      AddressLine2 = student.Address!.AddressLine2,
                                      PostalCode = student.Address!.PostalCode,
                                      City = student.Address!.City,
                                      Country = student.Address!.Country,
                                      StudentId = student.Id,
                                  } : null,
                              }).FirstOrDefaultAsync();
                              
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
                                  join address in _Db.Addresses
                                    on student.Id equals address.StudentId into addressGroup
                                  from address in addressGroup.DefaultIfEmpty()
                                  join user in _Db.Users
                                    on student.Id equals user.Id into userGroup
                                  from user in userGroup.DefaultIfEmpty() 
                                  where student.IsActive != false
                                  orderby student.CteatedDate descending
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
                                      Address = address != null ? new AddressResponseDTO()
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
