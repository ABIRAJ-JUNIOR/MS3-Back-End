using MS3_Back_End.DBContext;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.RequestDTOs.Student;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;

namespace MS3_Back_End.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _StudentRepo;
        public StudentService(IStudentRepository StudentRepo)
        {
            _StudentRepo = StudentRepo;
        }

        public async Task<StudentResponseDTO> AddStudent(StudentRequestDTO Stundent)
        {

            var Student = new Student
            {
               
            };

            var data = await _StudentRepo.AddStudent(Student);

            var StudentReponse = new StudentResponseDTO
            {
               
            };

            return StudentReponse;

        }



    }
}
