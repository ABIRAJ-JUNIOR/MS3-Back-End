using MS3_Back_End.DTOs.RequestDTOs.CourseCategory;
using MS3_Back_End.DTOs.ResponseDTOs.CourseCategory;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class CourseCategoryService : ICourseCategoryService
    {
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        private readonly ILogger<CourseCategoryService> _logger;

        public CourseCategoryService(ICourseCategoryRepository courseCategoryRepository, ILogger<CourseCategoryService> logger)
        {
            _courseCategoryRepository = courseCategoryRepository;
            _logger = logger;
        }

        public async Task<CourseCategoryResponseDTO> AddCategory(CourseCategoryRequestDTO courseCategoryRequestDTO)
        {
            if (courseCategoryRequestDTO == null)
            {
                throw new ArgumentNullException(nameof(courseCategoryRequestDTO));
            }

            var category = new CourseCategory
            {
                CategoryName = courseCategoryRequestDTO.CategoryName,
                Description = courseCategoryRequestDTO.Description
            };

            var data = await _courseCategoryRepository.AddCategory(category);

            var courseCategoryResponse = new CourseCategoryResponseDTO
            {
                Id = data.Id,
                CategoryName = data.CategoryName,
                Description = data.Description
            };

            _logger.LogInformation("Category added successfully with Id: {Id}", data.Id);

            return courseCategoryResponse;
        }

        public async Task<CourseCategoryResponseDTO> GetCourseCategoryById(Guid id)
        {
            var data = await _courseCategoryRepository.GetCourseCategoryById(id);
            if (data == null)
            {
                _logger.LogWarning("Category not found for Id: {Id}", id);
                throw new KeyNotFoundException("Category not found");
            }

            var courseCategoryResponse = new CourseCategoryResponseDTO
            {
                Id = data.Id,
                CategoryName = data.CategoryName,
                Description = data.Description
            };

            return courseCategoryResponse;
        }

        public async Task<CourseCategoryResponseDTO> UpdateCourseCategory(CategoryUpdateRequestDTO courseCategoryRequestDTO)
        {
            if (courseCategoryRequestDTO == null)
            {
                throw new ArgumentNullException(nameof(courseCategoryRequestDTO));
            }

            var getData = await _courseCategoryRepository.GetCourseCategoryById(courseCategoryRequestDTO.Id);
            if (getData == null)
            {
                _logger.LogWarning("Category not found for Id: {Id}", courseCategoryRequestDTO.Id);
                throw new KeyNotFoundException("Category not found");
            }

            getData.CategoryName = courseCategoryRequestDTO.CategoryName;
            getData.Description = courseCategoryRequestDTO.Description;

            var updatedData = await _courseCategoryRepository.UpdateCourseCategory(getData);

            var courseCategoryResponse = new CourseCategoryResponseDTO
            {
                Id = updatedData.Id,
                CategoryName = updatedData.CategoryName,
                Description = updatedData.Description
            };

            _logger.LogInformation("Category updated successfully with Id: {Id}", updatedData.Id);

            return courseCategoryResponse;
        }

        public async Task<List<CourseCategoryResponseDTO>> GetAllGetCourseCategory()
        {
            var data = await _courseCategoryRepository.GetAllGetCourseCategory();
            if (data == null || !data.Any())
            {
                _logger.LogWarning("No categories found");
                throw new KeyNotFoundException("No categories found");
            }

            var returnData = data.Select(c => new CourseCategoryResponseDTO
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                Description = c.Description
            }).ToList();

            return returnData;
        }
    }
}
