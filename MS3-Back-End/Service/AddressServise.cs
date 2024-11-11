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
        public async Task<AddressResponseDTO> GetAddressbyStuID(Guid  id)
        {
            var data = await _addressRepository.GetAddressbyStuID(id);
            var Returndata = new AddressResponseDTO()
            {
                AddressLine1 = data.AddressLine1,
                AddressLine2 = data.AddressLine2,
                City = data.City,
                PostalCode = data.PostalCode,
                Country = data.Country,
                StudentId = data.StudentId,
            };
            return Returndata;
        }
        public async Task<List<AddressResponseDTO>> GetAllAddress()
        {
              var data= await _addressRepository.GetAllAddress();
               var Returndata = data.Select(x => new AddressResponseDTO()
               {
                   AddressLine1 = x.AddressLine1,
                   AddressLine2 = x.AddressLine2,
                   City = x.City,
                   PostalCode = x.PostalCode,
                   Country = x.Country,
                   StudentId=x.StudentId,
                   

               }).ToList();

              return Returndata;
        }



    }
}
