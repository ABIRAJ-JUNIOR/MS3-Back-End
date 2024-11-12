using MS3_Back_End.DBContext;
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

    }
}
