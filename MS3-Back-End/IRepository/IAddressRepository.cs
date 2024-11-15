using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IAddressRepository
    {
        Task<Address> AddAddress(Address address);
        Task<Address> GetAddressByID(Guid id);
        Task<Address> DeleteAddress(Address address);
        Task<Address> UpdateAddress(Address address);
    }
}
