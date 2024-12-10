using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs;
using MS3_Back_End.DTOs.RequestDTOs.Announcement;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Announcement;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                Description = AnnouncementReq.Description,
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
                Description = data.Description,
                DatePosted = data.DatePosted,
                ExpirationDate = data.ExpirationDate,
                AudienceType = ((AudienceType)data.AudienceType).ToString(),
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
                Description = item.Description,
                DatePosted = item.DatePosted,
                AudienceType = ((AudienceType)item.AudienceType).ToString(),
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
                Description = item.Description,
                DatePosted = item.DatePosted,
                AudienceType = ((AudienceType)item.AudienceType).ToString(),
                ExpirationDate = item.ExpirationDate,
                IsActive = item.IsActive
            }).ToList();

            return AnnouncementResponse;
        }


        public async Task<AnnouncementResponseDTO> GetAnnouncementById(Guid id)
        {
            var data = await _AnnouncementRepo.GetAnnouncemenntByID(id);
            if (data == null)
            {
                throw new Exception("Announcement Not Found");
            }
            var AnnouncementReponse = new AnnouncementResponseDTO
            {
                Id = data.Id,
                Title = data.Title,
                Description = data.Description,
                DatePosted = data.DatePosted,
                AudienceType = ((AudienceType)data.AudienceType).ToString(),
                ExpirationDate = data.ExpirationDate,
                IsActive = data.IsActive
            };
            return AnnouncementReponse;
        }



        public async Task<ICollection<AnnouncementResponseDTO>> RecentAnnouncement()
        {

            var GetData = await _AnnouncementRepo.RecentAnnouncement();


            return GetData.Select(a => new AnnouncementResponseDTO()
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                DatePosted = a.DatePosted,
                ExpirationDate = a.ExpirationDate,
                AudienceType = ((AudienceType)a.AudienceType).ToString(),
                IsActive = a.IsActive
            }).ToList();

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
        public async Task<PaginationResponseDTO<AnnouncementResponseDTO>> GetPaginatedAnnouncement(int pageNumber, int pageSize ,string? role)
        {
            ICollection<Announcement> AllAnouncements;

            if (role == null)
            {
                AllAnouncements = await _AnnouncementRepo.GetAllAnnouncement();
            }
            else
            {
                AllAnouncements = await _AnnouncementRepo.GetAnnouncementsByRole(role);
            }

            var data = await _AnnouncementRepo.GetPaginatedAnnouncement(pageNumber, pageSize, role!);
            var returndata = data.Select(x => new AnnouncementResponseDTO
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                DatePosted = x.DatePosted,
                ExpirationDate = x.ExpirationDate,
                AudienceType = ((AudienceType)x.AudienceType).ToString(),
                IsActive = x.IsActive
            }).ToList();

            var PaginationResponseDTO = new PaginationResponseDTO<AnnouncementResponseDTO>
            {
                Items = returndata,
                PageSize = pageSize,
                CurrentPage = pageNumber,  
                TotalPages = (int)Math.Ceiling(AllAnouncements.Count / (double)pageSize),
                TotalItem = AllAnouncements.Count,
            };
            return PaginationResponseDTO;
        }

        public async Task<ICollection<AnnouncementResponseDTO>> AnnouncementValidCheck()
        {
            var announcements =await GetAllAnnouncement();
            foreach (var item in announcements)
            {
                if (item.DatePosted >  DateTime.Now)
                {
                    await DeleteAnnouncement(item.Id);
                }
            }
            return announcements;
        }
    }
}
