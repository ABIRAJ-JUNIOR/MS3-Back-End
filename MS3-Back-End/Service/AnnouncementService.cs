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
        private readonly IAnnouncementRepository _announcementRepo;
        private readonly ILogger<AnnouncementService> _logger;

        public AnnouncementService(IAnnouncementRepository announcementRepo, ILogger<AnnouncementService> logger)
        {
            _announcementRepo = announcementRepo;
            _logger = logger;
        }

        public async Task<AnnouncementResponseDTO> AddAnnouncement(AnnouncementRequestDTO announcementReq)
        {
            if (announcementReq == null)
            {
                throw new ArgumentNullException(nameof(announcementReq));
            }

            var announcement = new Announcement
            {
                Title = announcementReq.Title,
                Description = announcementReq.Description,
                DatePosted = DateTime.Now,
                ExpirationDate = announcementReq.ExpirationDate,
                AudienceType = announcementReq.AudienceType,
                IsActive = true
            };

            var data = await _announcementRepo.AddAnnouncement(announcement);

            var announcementResponse = new AnnouncementResponseDTO
            {
                Id = data.Id,
                Title = data.Title,
                Description = data.Description,
                DatePosted = data.DatePosted,
                ExpirationDate = data.ExpirationDate,
                AudienceType = ((AudienceType)data.AudienceType).ToString(),
                IsActive = data.IsActive
            };

            _logger.LogInformation("Announcement added successfully with Id: {Id}", data.Id);

            return announcementResponse;
        }

        public async Task<ICollection<AnnouncementResponseDTO>> SearchAnnouncement(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                throw new ArgumentException("Search text cannot be null or empty", nameof(searchText));
            }

            var data = await _announcementRepo.SearchAnnouncements(searchText);
            if (data == null || !data.Any())
            {
                _logger.LogWarning("No announcements found for search text: {SearchText}", searchText);
                throw new KeyNotFoundException("Search not found");
            }

            var announcementResponse = data.Select(item => new AnnouncementResponseDTO
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                DatePosted = item.DatePosted,
                AudienceType = ((AudienceType)item.AudienceType).ToString(),
                ExpirationDate = item.ExpirationDate,
                IsActive = item.IsActive
            }).ToList();

            return announcementResponse;
        }

        public async Task<ICollection<AnnouncementResponseDTO>> GetAllAnnouncement()
        {
            var data = await _announcementRepo.GetAllAnnouncement();
            if (data == null || !data.Any())
            {
                _logger.LogWarning("No announcements available");
                throw new KeyNotFoundException("Announcement not available");
            }

            var announcementResponse = data.Select(item => new AnnouncementResponseDTO
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                DatePosted = item.DatePosted,
                AudienceType = ((AudienceType)item.AudienceType).ToString(),
                ExpirationDate = item.ExpirationDate,
                IsActive = item.IsActive
            }).ToList();

            return announcementResponse;
        }

        public async Task<AnnouncementResponseDTO> GetAnnouncementById(Guid id)
        {
            var data = await _announcementRepo.GetAnnouncemenntByID(id);
            if (data == null)
            {
                _logger.LogWarning("Announcement not found for Id: {Id}", id);
                throw new KeyNotFoundException("Announcement not found");
            }

            var announcementResponse = new AnnouncementResponseDTO
            {
                Id = data.Id,
                Title = data.Title,
                Description = data.Description,
                DatePosted = data.DatePosted,
                AudienceType = ((AudienceType)data.AudienceType).ToString(),
                ExpirationDate = data.ExpirationDate,
                IsActive = data.IsActive
            };

            return announcementResponse;
        }

        public async Task<ICollection<AnnouncementResponseDTO>> RecentAnnouncement(AudienceType type)
        {
            var data = await _announcementRepo.RecentAnnouncement(type);

            var announcementResponse = data.Select(a => new AnnouncementResponseDTO
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                DatePosted = a.DatePosted,
                ExpirationDate = a.ExpirationDate,
                AudienceType = ((AudienceType)a.AudienceType).ToString(),
                IsActive = a.IsActive
            }).ToList();

            return announcementResponse;
        }

        public async Task<string> DeleteAnnouncement(Guid id)
        {
            var data = await _announcementRepo.GetAnnouncemenntByID(id);
            if (data == null)
            {
                _logger.LogWarning("Announcement not found for Id: {Id}", id);
                throw new KeyNotFoundException("Announcement not found");
            }

            data.IsActive = false;
            await _announcementRepo.DeleteAnnouncement(data);

            _logger.LogInformation("Announcement deleted successfully with Id: {Id}", id);

            return "Announcement deleted successfully";
        }

        public async Task<PaginationResponseDTO<AnnouncementResponseDTO>> GetPaginatedAnnouncement(int pageNumber, int pageSize, string? role)
        {
            ICollection<Announcement> allAnnouncements;

            if (string.IsNullOrWhiteSpace(role))
            {
                allAnnouncements = await _announcementRepo.GetAllAnnouncement();
            }
            else
            {
                allAnnouncements = await _announcementRepo.GetAnnouncementsByRole(role);
            }

            var data = await _announcementRepo.GetPaginatedAnnouncement(pageNumber, pageSize, role!);
            var returnData = data.Select(x => new AnnouncementResponseDTO
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                DatePosted = x.DatePosted,
                ExpirationDate = x.ExpirationDate,
                AudienceType = ((AudienceType)x.AudienceType).ToString(),
                IsActive = x.IsActive
            }).ToList();

            var paginationResponseDTO = new PaginationResponseDTO<AnnouncementResponseDTO>
            {
                Items = returnData,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(allAnnouncements.Count / (double)pageSize),
                TotalItem = allAnnouncements.Count,
            };

            return paginationResponseDTO;
        }

        public async Task<string> AnnouncementValidCheck()
        {
            var announcements = await GetAllAnnouncement();

            foreach (var item in announcements)
            {
                if (item.ExpirationDate <= DateTime.UtcNow)
                {
                    await DeleteAnnouncement(item.Id);
                }
            }

            _logger.LogInformation("Announcement validation completed successfully");

            return "Announcement validation successful";
        }
    }
}
