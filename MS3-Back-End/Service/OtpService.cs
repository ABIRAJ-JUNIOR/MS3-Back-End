using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class OtpService : IOtpService
    {
        private readonly IOtpRepository _repository;
        public OtpService(IOtpRepository repo)
        {
            _repository = repo;
        }



    }
}
