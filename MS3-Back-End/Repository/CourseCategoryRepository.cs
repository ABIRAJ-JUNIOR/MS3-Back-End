using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class CourseCategoryRepository: ICourseCategoryRepository
    {
        private readonly AppDBContext _appDBContext;

        public CourseCategoryRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<CourseCategory> AddCategory(CourseCategory categoryReq)
        {
            var categoryName = await _appDBContext.CourseCategories.SingleOrDefaultAsync(n => n.CategoryName == categoryReq.CategoryName);
            if (categoryName == null)
            {
                var data = await _appDBContext.CourseCategories.AddAsync(categoryReq);
                await _appDBContext.SaveChangesAsync();
                return data.Entity;
            }
            else
            {
                throw new Exception("Your Course Category Already Added");
            }
        }

        public async Task<CourseCategory> GetCourseCategoryById(Guid Id)
        {
            var data = await _appDBContext.CourseCategories.SingleOrDefaultAsync(c => c.Id == Id);
            return data;
        }

        public async Task<CourseCategory> UpdateCourseCategory(CourseCategory courseCategory)
        {
            var data = _appDBContext.CourseCategories.Update(courseCategory);
            await _appDBContext.SaveChangesAsync();
            return data.Entity;
        }
    }

}
}
