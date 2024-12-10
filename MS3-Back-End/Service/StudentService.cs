using Azure.Core;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Hosting;
using MS3_Back_End.DBContext;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.RequestDTOs.Student;
using MS3_Back_End.DTOs.ResponseDTOs.Address;
using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Enrollment;
using MS3_Back_End.DTOs.ResponseDTOs.Payment;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
using MS3_Back_End.DTOs.ResponseDTOs.StudentAssessment;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MS3_Back_End.DTOs.RequestDTOs.Admin;
using MS3_Back_End.DTOs.ResponseDTOs.Admin;
using MS3_Back_End.DTOs.RequestDTOs.password_student;
using MS3_Back_End.DTOs.Email;

namespace MS3_Back_End.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _StudentRepo;
        private readonly IAuthRepository _authRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly SendMailService _sendMailService;

        public StudentService(IStudentRepository studentRepo, IAuthRepository authRepository, INotificationRepository notificationRepository, SendMailService sendMailService)
        {
            _StudentRepo = studentRepo;
            _authRepository = authRepository;
            _notificationRepository = notificationRepository;
            _sendMailService = sendMailService;
        }

        public async Task<StudentResponseDTO> AddStudent(StudentRequestDTO StudentReq)
        {
            var nicCheck = await _authRepository.GetStudentByNic(StudentReq.Nic);
            var emailCheck = await _authRepository.GetUserByEmail(StudentReq.Email);

            if (nicCheck != null)
            {
                throw new Exception("Nic already used");
            }

            if (emailCheck != null)
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
                ImageUrl = null,
                CteatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,

            };

            if (StudentReq.Address != null)
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

            string NotificationMessage = $@"
Subject: 🎉 Welcome to Way Makers!<br><br>

Dear {data.FirstName} {data.LastName},<br><br>

We are thrilled to welcome you to Way Makers, where learning meets excellence!<br><br>

As a valued student, you now have access to:<br>
✅ A wide range of industry-relevant courses.<br>
✅ Expert instructors to guide your learning journey.<br><br>

Here’s how to get started:<br>
1. Log in to your account using your credentials.<br>
2. Explore available courses.<br>
3. Stay updated with announcements.<br><br>

We are committed to empowering your educational journey and helping you achieve your goals.<br><br>

For assistance, feel free to contact us at info.way.mmakers@gmail.com or call 0702274212.<br><br>

Once again, welcome to the Way Makers family! 🎓<br><br>

Warm regards,<br>
Way Makers<br>
Empowering learners, shaping futures.
";

            var Message = new Notification
            {
                Message = NotificationMessage,
                NotificationType = NotificationType.WelCome,
                StudentId = data.Id,
                DateSent = DateTime.Now,
                IsRead = false
            };

            await _notificationRepository.AddNotification(Message);

            var verifyMail = new SendVerifyMailRequest()
            {
                Name = data.FirstName + " " + data.LastName,
                Email = userData.Email,
                VerificationLink = $"http://localhost:4200/email-verified/{userData.Id}",
                EmailType = EmailTypes.EmailVerification,
            };

            await _sendMailService.VerifyMail(verifyMail);

            var StudentReponse = new StudentResponseDTO
            {
                Id = data.Id,
                Nic = data.Nic,
                FirstName = data.FirstName,
                LastName = data.LastName,
                DateOfBirth = data.DateOfBirth,
                Gender = ((Gender)data.Gender).ToString(),
                Phone = data.Phone,
                ImageUrl = data.ImageUrl!,
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

        public async Task<ICollection<StudentResponseDTO>> SearchStudent(string SearchText)
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
                Gender = ((Gender)item.Gender).ToString(),
                Phone = item.Phone,
                ImageUrl = item.ImageUrl!,
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

        public async Task<ICollection<StudentResponseDTO>> GetAllStudent()
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
                Gender = ((Gender)item.Gender).ToString(),
                Phone = item.Phone,
                ImageUrl = item.ImageUrl!,
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


        public async Task<StudentFullDetailsResponseDTO> GetStudentFullDetailsById(Guid StudentId)
        {
            var item = await _StudentRepo.GetStudentFullDetailsById(StudentId);
            if (item == null)
            {
                throw new Exception("Student Not Found");
            }
            return item;
        }

        public async Task<StudentResponseDTO> UpdateStudentFullDetails(Guid id, StudentFullUpdateDTO request)
        {
            var studentData = await _StudentRepo.GetStudentById(id);

            if (studentData == null)
            {
                throw new Exception("Student not found");
            }

            studentData.FirstName = request.FirstName;
            studentData.Gender = request.Gender;
            studentData.LastName = request.LastName;
            studentData.Phone = request.Phone;
            studentData.UpdatedDate = DateTime.Now;
            if (request.Address != null)
            {
                studentData.Address = new Address
                {
                    AddressLine1 = request.Address.AddressLine1,
                    AddressLine2 = request.Address.AddressLine2,
                    PostalCode = request.Address.PostalCode,
                    City = request.Address.City,
                    Country = request.Address.Country,
                };
            }

            var updatedData = await _StudentRepo.UpdateStudent(studentData);

            var userData = await _authRepository.GetUserById(id);
            if (userData == null)
            {
                throw new Exception("User not found");
            }

            if (userData.Password != null)
            {
                userData.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }

            var userUpdateData = await _authRepository.UpdateUser(userData);


            var StudentReponse = new StudentResponseDTO
            {
                Id = updatedData.Id,
                Nic = updatedData.Nic,
                FirstName = updatedData.FirstName,
                LastName = updatedData.LastName,
                DateOfBirth = updatedData.DateOfBirth,
                Gender = ((Gender)updatedData.Gender).ToString(),
                Phone = updatedData.Phone,
                ImageUrl = updatedData.ImageUrl!,
                CteatedDate = updatedData.CteatedDate,
                UpdatedDate = updatedData.UpdatedDate,
            };

            if (updatedData.Address != null)
            {
                var AddressResponse = new AddressResponseDTO
                {
                    StudentId = updatedData.Address.StudentId,
                    AddressLine1 = updatedData.Address.AddressLine1,
                    AddressLine2 = updatedData.Address.AddressLine2,
                    PostalCode = updatedData.Address.PostalCode,
                    City = updatedData.Address.City,
                    Country = updatedData.Address.Country,
                };

                StudentReponse.Address = AddressResponse;
            }

            return StudentReponse;
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
                Gender = ((Gender)item.Gender).ToString(),
                Phone = item.Phone,
                ImageUrl = item.ImageUrl!,
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

        public async Task<PaginationResponseDTO<StudentWithUserResponseDTO>> GetPaginatedStudent(int pageNumber, int pageSize)

        {

            var AllStudents = await _StudentRepo.GetAllStudente();

            if (AllStudents == null)
            {
                throw new Exception("Students Not Found");
            }
            var Students = await _StudentRepo.GetPaginatedStudent(pageNumber, pageSize);

            var paginationResponseDto = new PaginationResponseDTO<StudentWithUserResponseDTO>

            {
                Items = Students,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(AllStudents.Count / (double)pageSize),
                TotalItem = AllStudents.Count,
            };

            return paginationResponseDto;
        }

        public async Task<string> UploadImage(Guid studentId, IFormFile? image)
        {
            var studentData = await _StudentRepo.GetStudentById(studentId);
            if (studentData == null)
            {
                throw new Exception("Student not found");
            }

            if (image == null)
            {
                throw new Exception("Could not upload image");
            }

            var cloudinaryUrl = "cloudinary://779552958281786:JupUDaXM2QyLcruGYFayOI1U9JI@dgpyq5til";

            Cloudinary cloudinary = new Cloudinary(cloudinaryUrl);

            using (var stream = image.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, stream),
                    UseFilename = true,
                    UniqueFilename = true,
                    Overwrite = true
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                studentData.ImageUrl = (uploadResult.SecureUrl).ToString();
            }
            var updatedData = await _StudentRepo.UpdateStudent(studentData);

            return "Image upload successfully";
        }
        public async Task<StudentResponseDTO> UpdateStudentInfoDetails(Guid id, StudentFullUpdateDTO request)
        {
            var studentData = await _StudentRepo.GetStudentById(id);

            if (studentData == null)
            {
                throw new Exception("Student not found");
            }

            studentData.FirstName = request.FirstName;
            studentData.Gender = request.Gender;
            studentData.LastName = request.LastName;
            studentData.Phone = request.Phone;
            studentData.UpdatedDate = DateTime.Now;
            if (request.Address != null)
            {
                studentData.Address = new Address
                {
                    AddressLine1 = request.Address.AddressLine1,
                    AddressLine2 = request.Address.AddressLine2,
                    PostalCode = request.Address.PostalCode,
                    City = request.Address.City,
                    Country = request.Address.Country,
                };
            }

            var updatedData = await _StudentRepo.UpdateStudent(studentData);


            var StudentReponse = new StudentResponseDTO
            {
                Id = updatedData.Id,
                Nic = updatedData.Nic,
                FirstName = updatedData.FirstName,
                LastName = updatedData.LastName,
                DateOfBirth = updatedData.DateOfBirth,
                Gender = ((Gender)updatedData.Gender).ToString(),
                Phone = updatedData.Phone,
                ImageUrl = updatedData.ImageUrl!,
                CteatedDate = updatedData.CteatedDate,
                UpdatedDate = updatedData.UpdatedDate,
            };

            if (updatedData.Address != null)
            {
                var AddressResponse = new AddressResponseDTO
                {
                    StudentId = updatedData.Address.StudentId,
                    AddressLine1 = updatedData.Address.AddressLine1,
                    AddressLine2 = updatedData.Address.AddressLine2,
                    PostalCode = updatedData.Address.PostalCode,
                    City = updatedData.Address.City,
                    Country = updatedData.Address.Country,
                };

                StudentReponse.Address = AddressResponse;
            }

            return StudentReponse;
        }

         
        public async Task<string> UpdateStudentPassword(Guid studentId , PasswordRequest auth)
        {

            var GetData = await _authRepository.GetUserById(studentId);
            if (GetData == null)
            {
                throw new Exception("User is not valid");

            }
            var PasswordChecking = BCrypt.Net.BCrypt.Verify(auth.OldPassword, GetData.Password);
            if (PasswordChecking)
            {
                GetData.Password = BCrypt.Net.BCrypt.HashPassword(auth.ConfirmPassword);
                var response = await _authRepository.UpdateUser(GetData);
            }
            else
            {
                throw new Exception("your  password is not match please Try again Later");
            }
           
            return "Your Password Change Succesfully.";

        }
    }
}
