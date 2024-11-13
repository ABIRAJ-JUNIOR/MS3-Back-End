using MS3_Back_End.DBContext;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.RequestDTOs.Student;
using MS3_Back_End.DTOs.ResponseDTOs.Address;
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
        private readonly IAddressRepository _AddressRepo;
        public StudentService(IStudentRepository StudentRepo, IAddressRepository addressRepo)
        {
            _StudentRepo = StudentRepo;
            _AddressRepo = addressRepo;
        }

        public async Task<StudentResponseDTO> AddStudent(StudentRequestDTO StudentReq)
        {

            var Student = new Student
            {
                Nic = StudentReq.Nic,
                FirstName = StudentReq.FirstName,
                LastName = StudentReq.LastName,
                DateOfBirth = StudentReq.DateOfBirth,
                Gender = StudentReq.Gender,
                Phone = StudentReq.Phone,
                ImagePath = StudentReq.ImagePath,
                CteatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,

            };
            var data = await _StudentRepo.AddStudent(Student);

            var address = new Address
            {
                AddressLine1 = StudentReq.Address.AddressLine1,
                AddressLine2 = StudentReq.Address.AddressLine2,
                PostalCode = StudentReq.Address.PostalCode,
                City = StudentReq.Address.City,
                Country = StudentReq.Address.Country,
                StudentId = data.Id
            };

            var addressData =await _AddressRepo.AddAddress(address);

            var AddressResponse = new AddressResponseDTO
            {
                StudentId = addressData.StudentId,
                AddressLine1 = addressData.AddressLine1,
                AddressLine2 = addressData.AddressLine2,
                PostalCode = addressData.PostalCode,
                City = addressData.City,
                Country = addressData.Country,
            };

            var StudentReponse = new StudentResponseDTO
            {
                Id = data.Id,
                Nic = data.Nic,
                FirstName = data.FirstName,
                LastName = data.LastName,
                DateOfBirth = data.DateOfBirth,
                Gender = data.Gender,
                Phone = data.Phone,
                ImagePath = data.ImagePath,
                CteatedDate = data.CteatedDate,
                UpdatedDate = data.UpdatedDate,
                Address=AddressResponse,


            };

            return StudentReponse;

        }



        public async Task<List<StudentResponseDTO>> SearchStudent(string SearchText)
        {
            var data = await _StudentRepo.SearchStudent(SearchText);
            if (data == null)
            {
                throw new Exception("Search Not Found");
            }

            var StudentRes = new List<StudentResponseDTO>();
           
            foreach (var item in data)
            {
                var AddressResponse = new AddressResponseDTO
                {

                    AddressLine1 = item.Address.AddressLine1,
                    AddressLine2 = item.Address.AddressLine2,
                    PostalCode = item.Address.PostalCode,
                    City = item.Address.City,
                    Country = item.Address.Country,
                };
                var obj = new StudentResponseDTO
                {
                    Id = item.Id,
                    Nic = item.Nic,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    DateOfBirth = item.DateOfBirth,
                    Gender = item.Gender,
                    Phone = item.Phone,
                    ImagePath = item.ImagePath,
                    CteatedDate = item.CteatedDate,
                    UpdatedDate = item.UpdatedDate,
                    Address = AddressResponse,

                };

                StudentRes.Add(obj);

            }
            return StudentRes;

        }

        public async Task<List<StudentResponseDTO>> GetAllStudent()
        {
            var data = await _StudentRepo.GetAllStudente();
            if (data == null)
            {
                throw new Exception("Students data is Not Available");
            }
            var StudentRes = new List<StudentResponseDTO>();
            foreach (var item in data)
            {
                var AddressResponse = new AddressResponseDTO
                {

                    AddressLine1 = item.Address.AddressLine1,
                    AddressLine2 = item.Address.AddressLine2,
                    PostalCode = item.Address.PostalCode,
                    City = item.Address.City,
                    Country = item.Address.Country,
                };
                var obj = new StudentResponseDTO
                {
                    Id = item.Id,
                    Nic = item.Nic,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    DateOfBirth = item.DateOfBirth,
                    Gender = item.Gender,
                    Phone = item.Phone,
                    ImagePath = item.ImagePath,
                    CteatedDate = item.CteatedDate,
                    UpdatedDate = item.UpdatedDate,
                    Address = AddressResponse,

                };
                StudentRes.Add(obj);
            }
            return StudentRes;
        }


        public async Task<StudentResponseDTO> GetStudentById(Guid StudentId)
        {
            var item = await _StudentRepo.GetStudentById(StudentId);
            if (item == null)
            {
                throw new Exception("Student Not Found");
            }
            var AddressResponse = new AddressResponseDTO
            {

                AddressLine1 = item.Address.AddressLine1,
                AddressLine2 = item.Address.AddressLine2,
                PostalCode = item.Address.PostalCode,
                City = item.Address.City,
                Country = item.Address.Country,
            };
            var obj = new StudentResponseDTO
            {
                Id = item.Id,
                Nic = item.Nic,
                FirstName = item.FirstName,
                LastName = item.LastName,
                DateOfBirth = item.DateOfBirth,
                Gender = item.Gender,
                Phone = item.Phone,
                ImagePath = item.ImagePath,
                CteatedDate = item.CteatedDate,
                UpdatedDate = item.UpdatedDate,
                Address = AddressResponse,

            };
            return obj;
        }

        public async Task<StudentResponseDTO> UpdateStudent(StudentUpdateDTO studentUpdate)
        {


            var studentData = await _StudentRepo.GetStudentById(studentUpdate.Id);

            if (!string.IsNullOrEmpty(studentUpdate.Nic))
                studentData.Nic = studentUpdate.Nic;

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

            if (!string.IsNullOrEmpty(studentUpdate.ImagePath))
                studentData.ImagePath = studentUpdate.ImagePath;

            studentData.UpdatedDate = DateTime.Now;

            var item = await _StudentRepo.UpdateStudent(studentData);

            var AddressResponse = new AddressResponseDTO
            {

                AddressLine1 = item.Address.AddressLine1,
                AddressLine2 = item.Address.AddressLine2,
                PostalCode = item.Address.PostalCode,
                City = item.Address.City,
                Country = item.Address.Country,
            };
            var obj = new StudentResponseDTO
            {
                Id = item.Id,
                Nic = item.Nic,
                FirstName = item.FirstName,
                LastName = item.LastName,
                DateOfBirth = item.DateOfBirth,
                Gender = item.Gender,
                Phone = item.Phone,
                ImagePath = item.ImagePath,
                CteatedDate = item.CteatedDate,
                UpdatedDate = item.UpdatedDate,
                Address = AddressResponse,

            };
            return obj;

        }

        public async Task<string> DeleteStudent(Guid Id)
        {
            var GetData = await _StudentRepo.GetStudentById(Id);
            GetData.IsActive = false;
            if (GetData == null)
            {
                throw new Exception("Student Id not Found");
            }
            var data = await _StudentRepo.DeleteStudent(GetData);
            return data;
        }









    }
}
