using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class AddressServise: IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressServise(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
    }
}
