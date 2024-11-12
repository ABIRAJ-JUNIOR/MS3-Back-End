using MS3_Back_End.DTOs.RequestDTOs.Announcement;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Announcement;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.Repository;

namespace MS3_Back_End.Service
{
    public class AnnouncementService 
    {
        private readonly IAnnouncementRepository _AnnouncentRepo;
        public AnnouncementService(IAnnouncementRepository Announcement)
        {
            _AnnouncentRepo = Announcement;
        }


        public async Task<AnnouncementResponseDTO> AddCourse(AnnouncementRequestDTO AnnouncementReq)
        {

            var Announcement = new Announcement
            {
                Title = AnnouncementReq.Title,
                DatePosted = AnnouncementReq.DatePosted,
                AudienceType = AnnouncementReq.AudienceType

            };

            var data = await _AnnouncentRepo.AddAnnouncement(Announcement);


            var AnnouncementReponse = new AnnouncementResponseDTO
            {
                Id=data.Id,
                Title = data.Title,
                DatePosted = data.DatePosted,
                AudienceType = data.AudienceType,
                IsActive = data.IsActive
            };

            return AnnouncementReponse;

        }






    }
}
