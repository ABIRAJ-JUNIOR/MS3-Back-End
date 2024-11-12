using MS3_Back_End.DTOs.RequestDTOs.CourseCategory;
using MS3_Back_End.DTOs.ResponseDTOs.CourseCategory;

namespace MS3_Back_End.IService
{
    public interface ICourseCategoryService
    {
        Task<CourseCategoryResponseDTO> AddCategory(CourseCategoryRequestDTO courseCategoryRequestDTO);
        Task<CourseCategoryResponseDTO> GetCourseCategoryById(Guid Id);

    }
}
