using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs;
using MS3_Back_End.DTOs.RequestDTOs.Announcement;
using MS3_Back_End.DTOs.ResponseDTOs.Announcement;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IService
{
    public interface IAnnouncementService
    {
        Task<AnnouncementResponseDTO> AddAnnouncement(AnnouncementRequestDTO AnnouncementReq);
        Task<ICollection<AnnouncementResponseDTO>> SearchAnnouncement(string SearchText);
        Task<ICollection<AnnouncementResponseDTO>> GetAllAnnouncement();
        Task<AnnouncementResponseDTO> GetAnnouncementById(Guid id);
        Task<ICollection<AnnouncementResponseDTO>> RecentAnnouncement();
        Task<string> DeleteAnnouncement(Guid Id);
        Task<PaginationResponseDTO<AnnouncementResponseDTO>> GetPaginatedAnnouncement(int pageNumber, int pageSize, string role);


    }
}