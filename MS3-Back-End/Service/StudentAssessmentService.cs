using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class StudentAssessmentService : IStudentAssessmentService
    {
        private readonly IStudentAssessmentRepository _repository;

        public StudentAssessmentService(IStudentAssessmentRepository studentAssessmentRepository)
        {
            _repository = studentAssessmentRepository;
        }
    }
}
