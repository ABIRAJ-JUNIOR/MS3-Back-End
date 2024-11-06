using BCrypt.Net;
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
                        PostalCode = student.address.PostalCode,
                        Country = student.address.Country
                    };

                    var responseDTO = new StudentResponseDTO()
                    {
                        Id = student.Id,
                        Nic = student.Nic,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        DateOfBirth = student.DateOfBirth,
                        Gender = student.Gender,
                        Email = student.Email,
                        Phone = student.Phone,
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
                        DateOfBirth = student.DateOfBirth,
                        Gender = student.Gender,
                        Email = student.Email,
                        Phone = student.Phone,

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
                        PostalCode = student.address.PostalCode,
                        Country = student.address.Country
                    };

                    var studentObj = new StudentResponseDTO()
                    {
                        Id = student.Id,
                        Nic = student.Nic,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        DateOfBirth= student.DateOfBirth,
                        Gender = student.Gender,
                        Email = student.Email,
                        Phone = student.Phone,
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
                        DateOfBirth= student.DateOfBirth,
                        Gender = student.Gender,
                        Email = student.Email,
                        Phone = student.Phone,
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
                        PostalCode = studentRequest.address.PostalCode,
                        Country = studentRequest.address.Country,
                    };

                    var studentObj = new Student()
                    {
                        Nic = studentRequest.Nic,
                        FirstName = studentRequest.FirstName,
                        LastName = studentRequest.LastName,
                        DateOfBirth = studentRequest.DateOfBirth,
                        Gender = studentRequest.Gender,
                        Email = studentRequest.Email,
                        Phone = studentRequest.Phone,
                        Password = BCrypt.Net.BCrypt.HashPassword(studentRequest.Password),
                        CteatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        address = addressObj,
                    };

                    var studentDetails = await _studentRepository.AddStudent(studentObj);

                    var addressResponse = new AddressResponseDTO()
                    {
                        Id = studentDetails.address.Id,
                        AddressLine1 = studentDetails.address.AddressLine1,
                        AddressLine2 = studentDetails.address.AddressLine2,
                        City = studentDetails.address.City,
                        PostalCode= studentDetails.address.PostalCode,
                        Country = studentDetails.address.Country,
                    };

                    var studentResponseObj = new StudentResponseDTO()
                    {
                        Id = studentDetails.Id,
                        Nic = studentDetails.Nic,
                        FirstName = studentDetails.FirstName,
                        LastName = studentDetails.LastName,
                        DateOfBirth = studentDetails.DateOfBirth,
                        Gender = studentDetails.Gender,
                        Email = studentDetails.Email,
                        Phone = studentDetails.Phone,
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
                        DateOfBirth= studentRequest.DateOfBirth,
                        Gender = studentRequest.Gender,
                        Email = studentRequest.Email,
                        Phone = studentRequest.Phone,
                        Password = BCrypt.Net.BCrypt.HashPassword(studentRequest.Password),
                        CteatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    };

                    var studentDetails = await _studentRepository.AddStudent(studentObj);

                    var studentResponseObj = new StudentResponseDTO()
                    {
                        Id = studentDetails.Id,
                        Nic = studentDetails.Nic,
                        FirstName = studentDetails.FirstName,
                        LastName = studentDetails.LastName,
                        DateOfBirth=studentDetails.DateOfBirth,
                        Gender = studentDetails.Gender,
                        Email = studentDetails.Email,
                        Phone = studentDetails.Phone,
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
