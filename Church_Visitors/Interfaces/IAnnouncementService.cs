using Church_Visitors.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Church_Visitors.Interfaces
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<AnnouncementDTO>> GetAllAnnouncementsAsync();
        Task<IEnumerable<AnnouncementDTO>> GetAnnouncementsByDateCreatedAsync(DateTime dateCreated);
        Task<IEnumerable<AnnouncementDTO>> GetAnnouncementsByTodaysDateAsync();
        Task<AnnouncementDTO> GetAnnouncementByIdAsync(string id);
        Task CreateAnnouncementAsync(AnnouncementDTO announcement);
        Task UpdateAnnouncementAsync(AnnouncementDTO announcement);
        Task DeleteAnnouncementAsync(string id);
        Task<IEnumerable<VisitorDTO>> SearchAnnouncementsAsync(string searchText);
    }
}
