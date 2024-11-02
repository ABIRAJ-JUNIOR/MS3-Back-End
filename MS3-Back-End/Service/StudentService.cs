using MS3_Back_End.DTO.RequestDTOs;
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
                if (student.address != null)
                {
                    var AddressResponse = new AddressResponseDTO()
                    {
                        Id = student.address.Id,
                        AddressLine1 = student.address.AddressLine1,
                        AddressLine2 = student.address.AddressLine2,
                        City = student.address.City,
                        Country = student.address.Country
                    };

                    var responseDTO = new StudentResponseDTO()
                    {
                        Id = student.Id,
                        Nic = student.Nic,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        Gender = student.Gender,
                        Email = student.Email,
                        Phone = student.Phone,
                        Password = student.Password,
                        address = AddressResponse,

                    };
                    studentsList.Add(responseDTO);
                }
                else
                {
                    var responseDTO = new StudentResponseDTO()
                    {
                        Id = student.Id,
                        Nic = student.Nic,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        Gender = student.Gender,
                        Email = student.Email,
                        Phone = student.Phone,
                        Password = student.Password,

                    };
                    studentsList.Add(responseDTO);
                }
            }
            return studentsList;
        }

        public async Task<StudentResponseDTO> GetStudentByNic(string nic)
        {
            var student = await _studentRepository.GetStudentByNic(nic);

            if (student != null)
            {
                if(student.address != null)
                {
                    var AddressResponse = new AddressResponseDTO()
                    {
                        Id = student.address.Id,
                        AddressLine1 = student.address.AddressLine1,
                        AddressLine2 = student.address.AddressLine2,
                        City = student.address.City,
                        Country = student.address.Country
                    };

                    var studentObj = new StudentResponseDTO()
                    {
                        Id = student.Id,
                        Nic = student.Nic,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        Gender = student.Gender,
                        Email = student.Email,
                        Phone = student.Phone,
                        Password = student.Password,
                        address = AddressResponse,
                    };

                    return studentObj;
                }
                else
                {
                    var studentObj = new StudentResponseDTO()
                    {
                        Id = student.Id,
                        Nic = student.Nic,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        Gender = student.Gender,
                        Email = student.Email,
                        Phone = student.Phone,
                        Password = student.Password,
                    };
                    return studentObj;
                }
            }
            else
            {
                throw new Exception("Not Found");
            }
        }

        public async Task<StudentResponseDTO> AddStudent(StudentRequestDTO studentRequest)
        {
            var student = await _studentRepository.GetStudentByNic(studentRequest.Nic);
            if (student == null)
            {
                var addressObj = new Address();

                if (studentRequest.address != null)
                {
                    addressObj = new Address()
                    {
                        AddressLine1 = studentRequest.address.AddressLine1,
                        AddressLine2 = studentRequest.address.AddressLine2,
                        City = studentRequest.address.City,
                        Country = studentRequest.address.Country,
                    };

                    var studentObj = new Student()
                    {
                        Nic = studentRequest.Nic,
                        FirstName = studentRequest.FirstName,
                        LastName = studentRequest.LastName,
                        Gender = studentRequest.Gender,
                        Email = studentRequest.Email,
                        Phone = studentRequest.Phone,
                        Password = studentRequest.Password,
                        address = addressObj == null ? null : addressObj,
                    };

                    var studentDetails = await _studentRepository.AddStudent(studentObj);

                    var addressResponse = new AddressResponseDTO()
                    {
                        Id = studentDetails.address.Id,
                        AddressLine1 = studentDetails.address.AddressLine1,
                        AddressLine2 = studentDetails.address.AddressLine2,
                        City = studentDetails.address.City,
                        Country = studentDetails.address.Country,
                    };

                    var studentResponseObj = new StudentResponseDTO()
                    {
                        Id = studentDetails.Id,
                        Nic = studentDetails.Nic,
                        FirstName = studentDetails.FirstName,
                        LastName = studentDetails.LastName,
                        Gender = studentDetails.Gender,
                        Email = studentDetails.Email,
                        Phone = studentDetails.Phone,
                        Password = studentDetails.Password,
                        address = addressResponse,
                    };

                    return studentResponseObj;
                }
                else
                {
                    var studentObj = new Student()
                    {
                        Nic = studentRequest.Nic,
                        FirstName = studentRequest.FirstName,
                        LastName = studentRequest.LastName,
                        Gender = studentRequest.Gender,
                        Email = studentRequest.Email,
                        Phone = studentRequest.Phone,
                        Password = studentRequest.Password,
                    };

                    var studentDetails = await _studentRepository.AddStudent(studentObj);

                    var studentResponseObj = new StudentResponseDTO()
                    {
                        Id = studentDetails.Id,
                        Nic = studentDetails.Nic,
                        FirstName = studentDetails.FirstName,
                        LastName = studentDetails.LastName,
                        Gender = studentDetails.Gender,
                        Email = studentDetails.Email,
                        Phone = studentDetails.Phone,
                        Password = studentDetails.Password,
                    };

                    return studentResponseObj;
                }
                

                
            }
            else
            {
                throw new Exception("Student already exists");
            }

            
        }
    }
}
