using MS3_Back_End.DTOs.RequestDTOs;
using MS3_Back_End.DTOs.RequestDTOs.Announcement;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Announcement;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;

namespace MS3_Back_End.Service
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _AnnouncementRepo;
        public AnnouncementService(IAnnouncementRepository Announcement)
        {
            _AnnouncementRepo = Announcement;
        }


        public async Task<AnnouncementResponseDTO> AddAnnouncement(AnnouncementRequestDTO AnnouncementReq)
        {

            var Announcement = new Announcement
            {
                Title = AnnouncementReq.Title,
                DatePosted = DateTime.Now,
                ExpirationDate = AnnouncementReq.ExpirationDate,
                AudienceType = AnnouncementReq.AudienceType,
                IsActive = true
            };

            var data = await _AnnouncementRepo.AddAnnouncement(Announcement);

            var AnnouncementReponse = new AnnouncementResponseDTO
            {
                Id = data.Id,
                Title = data.Title,
                DatePosted = data.DatePosted,
                ExpirationDate = data.ExpirationDate,
                AudienceType = data.AudienceType,
                IsActive = data.IsActive
            };

            return AnnouncementReponse;

        }

        public async Task<ICollection<AnnouncementResponseDTO>> SearchAnnouncement(string SearchText)
        {
            var data = await _AnnouncementRepo.SearchAnnouncements(SearchText);
            if (data == null)
            {
                throw new Exception("Search Not Found");
            }

            var AnnouncementResponse = data.Select(item => new AnnouncementResponseDTO()
            {
                Id = item.Id,
                Title = item.Title,
                DatePosted = item.DatePosted,
                AudienceType = item.AudienceType,
                ExpirationDate = item.ExpirationDate,
                IsActive = item.IsActive
            }).ToList();

            return AnnouncementResponse;
        }


        public async Task<ICollection<AnnouncementResponseDTO>> GetAllAnnouncement()
        {
            var data = await _AnnouncementRepo.GetAllAnnouncement();
            if (data == null)
            {
                throw new Exception("Announcement Not Available");
            }
            var AnnouncementResponse = data.Select(item => new AnnouncementResponseDTO()
            {
                Id = item.Id,
                Title = item.Title,
                DatePosted = item.DatePosted,
                AudienceType = item.AudienceType,
                ExpirationDate = item.ExpirationDate,
                IsActive = item.IsActive
            }).ToList();

            return AnnouncementResponse;
        }


        public async Task<AnnouncementResponseDTO> GetAnnouncementById(Guid CourseId)
        {
            var data = await _AnnouncementRepo.GetAnnouncemenntByID(CourseId);
            if (data == null)
            {
                throw new Exception("Announcement Not Found");
            }
            var AnnouncementReponse = new AnnouncementResponseDTO
            {
                Id = data.Id,
                Title = data.Title,
                DatePosted = data.DatePosted,
                AudienceType = data.AudienceType,
                ExpirationDate = data.ExpirationDate,
                IsActive = data.IsActive
            };
            return AnnouncementReponse;
        }



        public async Task<AnnouncementResponseDTO> UpdateAnnouncement(AnnounceUpdateDTO announcement)
        {

            var GetData = await _AnnouncementRepo.GetAnnouncemenntByID(announcement.Id);

            if (announcement.Title != null)
            {
                GetData.Title = announcement.Title;
            }

            if (announcement.ExpirationDate.HasValue)
            {
                GetData.ExpirationDate = announcement.ExpirationDate.Value;
            }

            if (announcement.AudienceType != null)
            {
                GetData.AudienceType = announcement.AudienceType.Value;
            };

            GetData.DatePosted = DateTime.Now;

            var data = await _AnnouncementRepo.UpdateAnnouncement(GetData);

            var AnnouncementReturn = new AnnouncementResponseDTO
            {
                Id = data.Id,
                Title = data.Title,
                DatePosted = data.DatePosted,
                ExpirationDate = data.ExpirationDate,
                AudienceType = data.AudienceType,
                IsActive = data.IsActive
            };

            return AnnouncementReturn;

        }


        public async Task<string> DeleteAnnouncement(Guid Id)
        {
            var GetData = await _AnnouncementRepo.GetAnnouncemenntByID(Id);
            if (GetData == null)
            {
                throw new Exception("Announcement Not Found");
            }
            GetData.IsActive = false;
            var data = await _AnnouncementRepo.DeleteAnnouncement(GetData);
            return data;
        }
    }
}
