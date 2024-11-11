using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IAssessmentRepository _repository;

        public AssessmentService(IAssessmentRepository repository)
        {
            _repository = repository;
        }
    }
}
