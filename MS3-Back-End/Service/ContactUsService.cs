﻿using MS3_Back_End.DTOs.RequestDTOs.ContactUs;
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

        public async Task<ContactUsResponseDTO> AddMessage(ContactUsRequestDTO requestDTO)
        {
            var Message = new ContactUs
            {
                Name = requestDTO.Name,
                Email = requestDTO.Email,
                Message = requestDTO.Message,
                IsRead = false
            };

            var data = await _contactUsRepository.AddMessage(Message);

            var newContactUs = new ContactUsResponseDTO
            {
                Name = data.Name,
                Email = data.Email,
                Message = data.Message,
                DateSubmited = data.DateSubmited,
                IsRead = data.IsRead
            };
            return newContactUs;
        }

        public async Task<List<ContactUsResponseDTO>> GetAllMessages()
        {
            var allMessages = await _contactUsRepository.GetAllMessages();
            if (allMessages == null)
            {
                throw new Exception("No messages");
            }
            var ContactUsResponse = new List<ContactUsResponseDTO>();
            foreach (var message in allMessages)
            {
                var obj = new ContactUsResponseDTO
                {
                    Id = message.Id,
                    Name = message.Name,
                    Email = message.Email,
                    Message = message.Message,
                };
                ContactUsResponse.Add(obj);
            }
            return ContactUsResponse;
        }


    }
}
