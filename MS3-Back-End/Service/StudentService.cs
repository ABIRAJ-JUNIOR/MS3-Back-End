using MS3_Back_End.DTO.ResponseDTOs;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<ICollection<StudentResponseDTO>> GetAllStudents()
        {
            var students = await _studentRepository.GetAllStudents();

            var studentsList = new List<StudentResponseDTO>();
            foreach (var student in students)
            {
                var responseDTO = new StudentResponseDTO()
                {
                    Id = student.Id,
                    Nic = student.Nic,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email,
                    Phone = student.Phone,
                    Password = student.Password,
                    
                };
                studentsList.Add(responseDTO);
            }
            return studentsList;
        }

        public async Task<StudentResponseDTO> GetStudentByNic(string nic)
        {
            var student = await _studentRepository.GetStudentByNic(nic);

            if (student != null)
            {
                var studentObj = new StudentResponseDTO()
                {
                    Id = student.Id,
                    Nic = student.Nic,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email,
                    Phone = student.Phone,
                    Password = student.Password,
                };

                return studentObj;
            }
            else
            {
                throw new Exception("Not Found");
            }
        }
    }
}
