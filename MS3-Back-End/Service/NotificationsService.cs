﻿using MS3_Back_End.DTOs.RequestDTOs.Notification;
using MS3_Back_End.DTOs.ResponseDTOs.Notification;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class NotificationsService: INotificationsService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationsService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<NotificationResponceDTO> AddNotification(NotificationRequestDTO notificationDTO)
        {
            var notification=new Notification()
            {
                StudentId = notificationDTO.StudentId,
                Message = notificationDTO.Message,
                NotificationType = notificationDTO.NotificationType,
                DateSent=  DateOnly.FromDateTime(DateTime.Now),
                IsRead=false,
                
            };
            var data= await _notificationRepository.AddNotification(notification);

            var returndata = new NotificationResponceDTO()
            {
               Id=data.Id,
               DateSent=data.DateSent,
               IsRead=data.IsRead,
               Message=data.Message,
               NotificationType=data.NotificationType,
               StudentId=data.StudentId,
            };
            return returndata;


        }
        public async Task<List<NotificationResponceDTO>> GetNotificationBYStuID(Guid Id)
        {
            var datas = await _notificationRepository.GetNotificationBYStuID(Id);
            var retutndata = datas.Select(x => new NotificationResponceDTO()
            {
                StudentId = x.StudentId,
                Message = x.Message,
                NotificationType = x.NotificationType,
                DateSent = x.DateSent,
                IsRead = x.IsRead,
                Id = x.Id


            }).ToList();

            return retutndata;
        }

    }
}
