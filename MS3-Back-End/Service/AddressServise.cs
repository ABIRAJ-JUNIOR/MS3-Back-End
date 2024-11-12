using MS3_Back_End.DTOs.RequestDTOs.Address;
using MS3_Back_End.DTOs.ResponseDTOs.Address;
using MS3_Back_End.Entities;
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
        public async Task<AddressResponseDTO> AddAddress(AddressRequestDTO address)
        {
            var Address=new Address()
            {
                AddressLine1=address.AddressLine1,
                AddressLine2=address.AddressLine2,
                City=address.City,
                PostalCode=address.PostalCode,
                Country=address.Country,
                StudentId=address.StudentId,
            };
           
            var data=await _addressRepository.AddAddress(Address);

            var Returndata = new AddressResponseDTO()
            {
                StudentId = address.StudentId,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                PostalCode = address.PostalCode,
                Country = address.Country,
            };

            return Returndata;

        }

    }
}
