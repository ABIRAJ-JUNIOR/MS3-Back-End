using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class AddressRepository: IAddressRepository
    {
        private readonly AppDBContext _dbContext;

        public AddressRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Address> AddAddress(Address address) 
        {
            var Address =await _dbContext.Addresses.SingleOrDefaultAsync(f=>f.StudentId == address.StudentId);
            if (Address == null)
            {
                var data = await _dbContext.Addresses.AddAsync(Address);
                _dbContext.SaveChangesAsync();
                return data.Entity;

            }
            else 
            {
                throw new Exception("Address Already Added");
            }
        }
        public async Task<Address> GetAddressbyStuID(Guid id)
        {
            var Address = await _dbContext.Addresses.SingleOrDefaultAsync(f => f.StudentId == id);
            if (Address != null)
            {
                return Address;
            }
            else 
            {
                throw new Exception("Address not found");
               
            }

        }

    }
}
