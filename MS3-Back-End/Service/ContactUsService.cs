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
                DateSubmited = DateTime.Now,
                IsRead = false
            };

            var data = await _contactUsRepository.AddMessage(Message);

            var newContactUs = new ContactUsResponseDTO
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                Message = data.Message,
                DateSubmited = data.DateSubmited,
                IsRead = data.IsRead
            };
            return newContactUs;
        }

        public async Task<ICollection<ContactUsResponseDTO>> GetAllMessages()
        {
            var allMessages = await _contactUsRepository.GetAllMessages();
            if (allMessages == null)
            {
                throw new Exception("No messages");
            }
            
            var ContactUsResponse = allMessages.Select(message => new ContactUsResponseDTO()
            {
                Id = message.Id,
                Name = message.Name,
                Email = message.Email,
                Message = message.Message,
                DateSubmited = DateTime.Now,
                IsRead = false
            }).ToList();

            return ContactUsResponse;
        }

        public async Task<ContactUsResponseDTO> UpdateMessage(UpdateResponseRequestDTO request)
        {
            var GetData = await _contactUsRepository.GetMessageById(request.Id);
            if(GetData == null)
            {
                throw new Exception("Not found");
            }

            GetData.Response = request.Response;
            GetData.IsRead = true;

            var UpdatedData = await _contactUsRepository.UpdateMessage(GetData);

            var newUpdateMessage = new ContactUsResponseDTO
            {
                Id = UpdatedData.Id,
                Name = UpdatedData.Name,
                Email = UpdatedData.Email,
                Response = UpdatedData.Response,
                DateSubmited = DateTime.Now,
                Message = UpdatedData.Message,
                IsRead = UpdatedData.IsRead
            };
            return newUpdateMessage;
        }

    }
}
