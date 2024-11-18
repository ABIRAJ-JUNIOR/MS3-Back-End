using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using MS3_Back_End.DBContext;
using MS3_Back_End.DTOs.Image;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.RequestDTOs.Student;
using MS3_Back_End.DTOs.ResponseDTOs.Address;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MS3_Back_End.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _StudentRepo;
        private readonly IAuthRepository _authRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentService(IStudentRepository studentRepo, IAuthRepository authRepository, IWebHostEnvironment webHostEnvironment)
        {
            _StudentRepo = studentRepo;
            _authRepository = authRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<StudentResponseDTO> AddStudent(StudentRequestDTO StudentReq)
        {
            var nicCheck = await _authRepository.GetStudentByNic(StudentReq.Nic);
            var emailCheck = await _authRepository.GetUserByEmail(StudentReq.Email);

            if(nicCheck != null)
            {
                throw new Exception("Nic already used");
            }

            if(emailCheck != null)
            {
                throw new Exception("Email already used");
            }

            var user = new User()
            {
                Email = StudentReq.Email,
                IsConfirmEmail = false,
                Password = BCrypt.Net.BCrypt.HashPassword(StudentReq.Password),

            };

            var userData = await _authRepository.AddUser(user);
            var roleData = await _authRepository.GetRoleByName("Student");
            if (roleData == null)
            {
                throw new Exception("Role not found");
            }

            var userRole = new UserRole()
            {
                UserId = userData.Id,
                RoleId = roleData.Id
            };

            var userRoleData = await _authRepository.AddUserRole(userRole);

            var Student = new Student
            {
                Id = userData.Id,
                Nic = StudentReq.Nic,
                FirstName = StudentReq.FirstName,
                LastName = StudentReq.LastName,
                DateOfBirth = StudentReq.DateOfBirth,
                Gender = StudentReq.Gender,
                Phone = StudentReq.Phone,
                CteatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,

            };

            if(StudentReq.Address != null)
            {
                var address = new Address
                {
                    AddressLine1 = StudentReq.Address.AddressLine1,
                    AddressLine2 = StudentReq.Address.AddressLine2,
                    PostalCode = StudentReq.Address.PostalCode,
                    City = StudentReq.Address.City,
                    Country = StudentReq.Address.Country,
                };

                Student.Address = address;
            }

            var data = await _StudentRepo.AddStudent(Student);

            var StudentReponse = new StudentResponseDTO
            {
                Id = data.Id,
                Nic = data.Nic,
                FirstName = data.FirstName,
                LastName = data.LastName,
                DateOfBirth = data.DateOfBirth,
                Gender = data.Gender,
                Phone = data.Phone,
                ImagePath = data.ImagePath!,
                CteatedDate = data.CteatedDate,
                UpdatedDate = data.UpdatedDate,
            };

            if (data.Address != null)
            {
                var AddressResponse = new AddressResponseDTO
                {
                    StudentId = data.Address.StudentId,
                    AddressLine1 = data.Address.AddressLine1,
                    AddressLine2 = data.Address.AddressLine2,
                    PostalCode = data.Address.PostalCode,
                    City = data.Address.City,
                    Country = data.Address.Country,
                };

                StudentReponse.Address = AddressResponse;
            }

            return StudentReponse;
        }

        public async Task<List<StudentResponseDTO>> SearchStudent(string SearchText)
        {
            var data = await _StudentRepo.SearchStudent(SearchText);
            if (data == null)
            {
                throw new Exception("Search Not Found");
            }

            var StudentRes = data.Select(item => new StudentResponseDTO()
            {
                Id = item.Id,
                Nic = item.Nic,
                FirstName = item.FirstName,
                LastName = item.LastName,
                DateOfBirth = item.DateOfBirth,
                Gender = item.Gender,
                Phone = item.Phone,
                ImagePath = item.ImagePath!,
                CteatedDate = item.CteatedDate,
                UpdatedDate = item.UpdatedDate,
                Address = item.Address != null ? new AddressResponseDTO()
                {
                    AddressLine1 = item.Address.AddressLine1,
                    AddressLine2 = item.Address.AddressLine2,
                    PostalCode = item.Address.PostalCode,
                    City = item.Address.City,
                    Country = item.Address.Country,
                    StudentId = item.Id,
                } : null,
            }).ToList();
            return StudentRes;

        }

        public async Task<List<StudentResponseDTO>> GetAllStudent()
        {
            var data = await _StudentRepo.GetAllStudente();
            if (data == null)
            {
                throw new Exception("Students data is Not Available");
            }
            var StudentRes = data.Select(item => new StudentResponseDTO()
            {
                Id = item.Id,
                Nic = item.Nic,
                FirstName = item.FirstName,
                LastName = item.LastName,
                DateOfBirth = item.DateOfBirth,
                Gender = item.Gender,
                Phone = item.Phone,
                ImagePath = item.ImagePath!,
                CteatedDate = item.CteatedDate,
                UpdatedDate = item.UpdatedDate,
                Address = item.Address != null ? new AddressResponseDTO()
                {
                    AddressLine1 = item.Address.AddressLine1,
                    AddressLine2 = item.Address.AddressLine2,
                    PostalCode = item.Address.PostalCode,
                    City = item.Address.City,
                    Country = item.Address.Country,
                    StudentId = item.Id,
                } : null,
            }).ToList();
            return StudentRes;
        }


        public async Task<StudentResponseDTO> GetStudentById(Guid StudentId)
        {
            var item = await _StudentRepo.GetStudentById(StudentId);
            if (item == null)
            {
                throw new Exception("Student Not Found");
            }

            var obj = new StudentResponseDTO
            {
                Id = item.Id,
                Nic = item.Nic,
                FirstName = item.FirstName,
                LastName = item.LastName,
                DateOfBirth = item.DateOfBirth,
                Gender = item.Gender,
                Phone = item.Phone,
                ImagePath = item.ImagePath!,
                CteatedDate = item.CteatedDate,
                UpdatedDate = item.UpdatedDate,
            };

            if (item.Address != null)
            {
                var AddressResponse = new AddressResponseDTO()
                {
                    AddressLine1 = item.Address.AddressLine1,
                    AddressLine2 = item.Address.AddressLine2,
                    PostalCode = item.Address.PostalCode,
                    City = item.Address.City,
                    Country = item.Address.Country,
                    StudentId = item.Id,
                };

                obj.Address = AddressResponse;
            }

            return obj;
        }

        public async Task<StudentResponseDTO> UpdateStudent(StudentUpdateDTO studentUpdate)
        {
            var studentData = await _StudentRepo.GetStudentById(studentUpdate.Id);

            if (!string.IsNullOrEmpty(studentUpdate.FirstName))
                studentData.FirstName = studentUpdate.FirstName;

            if (!string.IsNullOrEmpty(studentUpdate.LastName))
                studentData.LastName = studentUpdate.LastName;

            if (studentUpdate.DateOfBirth.HasValue && studentUpdate.DateOfBirth != DateTime.MinValue)
                studentData.DateOfBirth = studentUpdate.DateOfBirth.Value;

            if (studentUpdate.Gender.HasValue)
                studentData.Gender = studentUpdate.Gender.Value;

            if (!string.IsNullOrEmpty(studentUpdate.Phone))
                studentData.Phone = studentUpdate.Phone;

            studentData.UpdatedDate = DateTime.Now;

            var item = await _StudentRepo.UpdateStudent(studentData);

            var obj = new StudentResponseDTO
            {
                Id = item.Id,
                Nic = item.Nic,
                FirstName = item.FirstName,
                LastName = item.LastName,
                DateOfBirth = item.DateOfBirth,
                Gender = item.Gender,
                Phone = item.Phone,
                ImagePath = item.ImagePath!,
                CteatedDate = item.CteatedDate,
                UpdatedDate = item.UpdatedDate

            };
            return obj;
        }

        public async Task<string> DeleteStudent(Guid Id)
        {
            var GetData = await _StudentRepo.GetStudentById(Id);
            if (GetData == null)
            {
                throw new Exception("Student Id not Found");
            }

            GetData.IsActive = false;

            var data = await _StudentRepo.DeleteStudent(GetData);
            return data;
        }

                
        public async Task<PaginationResponseDTO<StudentResponseDTO>> GetPaginatedStudent(int pageNumber, int pageSize)
        {

            var AllStudents = await _StudentRepo.GetAllStudente();

            if (AllStudents == null)
            {
                throw new Exception("Students Not Found");
            }
            var Students = await _StudentRepo.GetPaginatedStudent(pageNumber, pageSize);

            var StudentResponse = Students.Select(item => new StudentResponseDTO()
            {
                Id = item.Id,
                Nic = item.Nic,
                FirstName = item.FirstName,
                LastName = item.LastName,
                DateOfBirth = item.DateOfBirth,
                Gender = item.Gender,
                Phone = item.Phone,
                ImagePath = item.ImagePath!,
                CteatedDate = item.CteatedDate,
                UpdatedDate = item.UpdatedDate,
                Address = item.Address != null ? new AddressResponseDTO()
                {
                    AddressLine1 = item.Address.AddressLine1,
                    AddressLine2 = item.Address.AddressLine2,
                    PostalCode = item.Address.PostalCode,
                    City = item.Address.City,
                    Country = item.Address.Country,
                    StudentId = item.Id,
                } : null,
            }).ToList();

            var paginationResponseDto= new PaginationResponseDTO<StudentResponseDTO>
            {
                Items = StudentResponse,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(AllStudents.Count / (double)pageSize),
                TotalItem = AllStudents.Count,
            };

            return paginationResponseDto;
        }

        public async Task<string> UploadImage(Guid studentId, ImageRequestDTO request)
        {
            var studentData = await _StudentRepo.GetStudentById(studentId);
            if (studentData == null)
            {
                throw new Exception("Student not found");
            }

            studentData.ImagePath = request.ImageFile != null ? await SaveImageFile(request.ImageFile) : null;
            var updatedData = await _StudentRepo.UpdateStudent(studentData);

            return "Image upload successfully";
        }

        private async Task<string> SaveImageFile(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return string.Empty;

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Student");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string filePath = Path.Combine(uploadPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return $"/Student/{fileName}";
        }
    }
}
