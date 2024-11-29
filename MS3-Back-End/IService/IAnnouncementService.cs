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
        Task<AnnouncementResponseDTO> UpdateAnnouncement(AnnounceUpdateDTO announcement);
        Task<string> DeleteAnnouncement(Guid Id);
        Task<ICollection<AnnouncementResponseDTO>> GetPaginatedAnnouncement(int pageNumber, int pageSize);

    }
}