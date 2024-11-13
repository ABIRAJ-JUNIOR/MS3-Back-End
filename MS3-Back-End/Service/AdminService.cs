using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAuthRepository _authRepository;

        public AdminService(IAdminRepository adminRepository, IAuthRepository authRepository)
        {
            _adminRepository = adminRepository;
            _authRepository = authRepository;
        }


    }
}
