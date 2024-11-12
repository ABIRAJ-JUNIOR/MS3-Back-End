using MS3_Back_End.DTOs.RequestDTOs;
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

        public async Task<List<AnnouncementResponseDTO>> SearchAnnouncement(string SearchText)
        {
            var data = await _AnnouncentRepo.SearchAnnouncements(SearchText);
            if (data == null)
            {
                throw new Exception("Search Not Found");
            }

            var AnnouncementReponse = new List<AnnouncementResponseDTO>();
            foreach (var item in data)
            {
                var obj = new AnnouncementResponseDTO
                {
                    Id = item.Id,
                    Title = item.Title,
                    DatePosted = item.DatePosted,
                    AudienceType = item.AudienceType,
                    IsActive = item.IsActive
                };
                AnnouncementReponse.Add(obj);

            }
            return AnnouncementReponse;

        }


        public async Task<List<AnnouncementResponseDTO>> GetAllAnnouncement()
        {
            var data = await _AnnouncentRepo.GetAllAnnouncement();
            if (data == null)
            {
                throw new Exception("Announcement Not Available");
            }
            var AnnouncementReponse = new List<AnnouncementResponseDTO>();
            foreach (var item in data)
            {
                var obj = new AnnouncementResponseDTO
                {
                    Id = item.Id,
                    Title = item.Title,
                    DatePosted = item.DatePosted,
                    AudienceType = item.AudienceType,
                    IsActive = item.IsActive

                };
                AnnouncementReponse.Add(obj);
            }
            return AnnouncementReponse;
        }


        public async Task<AnnouncementResponseDTO> GetAnnouncementById(Guid CourseId)
        {
            var data = await _AnnouncentRepo.GetAnnouncemenntByID(CourseId);
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
                IsActive = data.IsActive
            };
            return AnnouncementReponse;
        }



        public async Task<AnnouncementResponseDTO> UpdateCourse(AnnounceUpdateDTO announcement)
        {

            var GetData = await _AnnouncentRepo.GetAnnouncemenntByID(announcement.Id);

            if (announcement.Title != null)
            {
                GetData.Title = announcement.Title;
            }

            if (announcement.ExpirationDate!=null)
            {
                GetData.ExpirationDate = announcement.ExpirationDate;
            }
            if (announcement.IsActive != null)
            {
                GetData.IsActive = announcement.IsActive;
            }
            if (announcement.AudienceType != null)
            {
                GetData.AudienceType = announcement.AudienceType;
            }

            GetData.DatePosted = DateTime.Now;

            var data = await _AnnouncentRepo.UpdateAnnouncement(GetData);

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



    }
}
