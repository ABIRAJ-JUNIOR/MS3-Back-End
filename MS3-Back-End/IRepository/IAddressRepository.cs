using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IAddressRepository
    {
        Task<Address> AddAddress(Address address);
        Task<Address> GetAddressbyStuID(Guid id);
        Task<List<Address>> GetAllAddress();
        Task<Address> DeleteAddress(Address address);




    }
}
