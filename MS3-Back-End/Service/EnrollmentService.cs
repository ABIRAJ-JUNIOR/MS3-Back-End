using MS3_Back_End.IRepository;

namespace MS3_Back_End.Service
{
    public class EnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }





    }
}
