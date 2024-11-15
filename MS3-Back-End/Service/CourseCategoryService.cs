using MS3_Back_End.DTOs.RequestDTOs.CourseCategory;
using MS3_Back_End.DTOs.ResponseDTOs.CourseCategory;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class CourseCategoryService: ICourseCategoryService
    {
        private readonly ICourseCategoryRepository _courseCategoryRepository;

        public CourseCategoryService(ICourseCategoryRepository courseCategoryRepository)
        {
            _courseCategoryRepository = courseCategoryRepository;
        }

        public async Task<CourseCategoryResponseDTO> AddCategory(CourseCategoryRequestDTO courseCategoryRequestDTO)
        {
            var Category = new CourseCategory
            {
                CategoryName = courseCategoryRequestDTO.CategoryName,
                Description = courseCategoryRequestDTO.Description
            };

            var data = await _courseCategoryRepository.AddCategory(Category);

            var CourseCategoryResponse = new CourseCategoryResponseDTO
            {
                Id = data.Id,
                CategoryName = data.CategoryName,
                Description = data.Description
            };
            return CourseCategoryResponse;
        }

        public async Task<CourseCategoryResponseDTO> GetCourseCategoryById(Guid Id)
        {
            var data = await _courseCategoryRepository.GetCourseCategoryById(Id);
            if (data == null)
            {
                throw new Exception("Category not found");
            }
            var CourseCategoryResponse = new CourseCategoryResponseDTO
            {
                Id = data.Id,
                CategoryName = data.CategoryName,
                Description = data.Description
            };
            return CourseCategoryResponse;
        }

        public async Task<CourseCategoryResponseDTO> UpdateCourseCategory(CategoryUpdateRequestDTO courseCategoryRequestDTO)
        {
            var GetData = await _courseCategoryRepository.GetCourseCategoryById(courseCategoryRequestDTO.Id);
            if(GetData == null)
            {
                throw new Exception("Category not found");
            }
            GetData.CategoryName = courseCategoryRequestDTO.CategoryName;
            GetData.Description = courseCategoryRequestDTO.Description;

            var UpdatedData = await _courseCategoryRepository.UpdateCourseCategory(GetData);

            var CourseCategoryResponse = new CourseCategoryResponseDTO
            {
                Id = UpdatedData.Id,
                CategoryName = UpdatedData.CategoryName,
                Description = UpdatedData.Description

            };
            return CourseCategoryResponse;
        }
    }
}
