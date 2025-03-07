using MS3_Back_End.DTOs.RequestDTOs.Address;
using MS3_Back_End.DTOs.ResponseDTOs.Address;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using Microsoft.Extensions.Logging;

namespace MS3_Back_End.Service
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressService> _logger;

        public AddressService(IAddressRepository addressRepository, ILogger<AddressService> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<AddressResponseDTO> AddAddress(AddressRequestDTO address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            var newAddress = new Address()
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                PostalCode = address.PostalCode,
                Country = address.Country,
                StudentId = address.StudentId,
            };

            var data = await _addressRepository.AddAddress(newAddress);

            var returnData = new AddressResponseDTO()
            {
                StudentId = data.StudentId,
                AddressLine1 = data.AddressLine1,
                AddressLine2 = data.AddressLine2,
                City = data.City,
                PostalCode = data.PostalCode,
                Country = data.Country,
            };

            _logger.LogInformation("Address added successfully for StudentId: {StudentId}", data.StudentId);

            return returnData;
        }

        public async Task<AddressResponseDTO> UpdateAddress(Guid id, AddressUpdateRequestDTO updateAddress)
        {
            if (updateAddress == null)
            {
                throw new ArgumentNullException(nameof(updateAddress));
            }

            var address = await _addressRepository.GetAddressByID(id);
            if (address == null)
            {
                _logger.LogWarning("Address not found for Id: {Id}", id);
                throw new KeyNotFoundException("Address not found");
            }

            address.AddressLine1 = updateAddress.AddressLine1;
            address.AddressLine2 = updateAddress.AddressLine2;
            address.City = updateAddress.City;
            address.Country = updateAddress.Country;
            address.PostalCode = updateAddress.PostalCode;

            var data = await _addressRepository.UpdateAddress(address);

            var returnData = new AddressResponseDTO()
            {
                AddressLine1 = data.AddressLine1,
                AddressLine2 = data.AddressLine2,
                City = data.City,
                PostalCode = data.PostalCode,
                Country = data.Country,
                StudentId = data.StudentId,
            };

            _logger.LogInformation("Address updated successfully for Id: {Id}", id);

            return returnData;
        }

        public async Task<AddressResponseDTO> DeleteAddress(Guid id)
        {
            var address = await _addressRepository.GetAddressByID(id);
            if (address == null)
            {
                _logger.LogWarning("Address not found for Id: {Id}", id);
                throw new KeyNotFoundException("Address not found");
            }

            var data = await _addressRepository.DeleteAddress(address);

            var returnData = new AddressResponseDTO()
            {
                AddressLine1 = data.AddressLine1,
                AddressLine2 = data.AddressLine2,
                City = data.City,
                PostalCode = data.PostalCode,
                Country = data.Country,
                StudentId = data.StudentId,
            };

            _logger.LogInformation("Address deleted successfully for Id: {Id}", id);

            return returnData;
        }
    }
}
