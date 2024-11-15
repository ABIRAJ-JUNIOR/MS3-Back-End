using MS3_Back_End.DTOs.RequestDTOs.Address;
using MS3_Back_End.DTOs.ResponseDTOs.Address;

namespace MS3_Back_End.IService
{
    public interface IAddressService
    {
        Task<AddressResponseDTO> AddAddress(AddressRequestDTO address);
        Task<AddressResponseDTO> UpdateAddress(AddressUpdateRequestDTO Updateaddress, Guid StuId);
        Task<AddressResponseDTO> DeleteAddress(Guid id);
    }
}
