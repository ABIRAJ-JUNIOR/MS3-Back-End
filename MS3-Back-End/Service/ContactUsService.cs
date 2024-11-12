using MS3_Back_End.DTOs.RequestDTOs.ContactUs;
using MS3_Back_End.DTOs.ResponseDTOs.ContactUs;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class ContactUsService: IContactUsService
    {
        public readonly IContactUsRepository _contactUsRepository;

        public ContactUsService(IContactUsRepository contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }


    }
}
