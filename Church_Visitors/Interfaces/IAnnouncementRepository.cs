using Church_Visitors.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church_Visitors.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<AnnouncementDTO>> GetAllAnnouncementsAsync();
        Task<IEnumerable<AnnouncementDTO>> GetAnnouncementsByDateCreatedAsync(DateTime dateCreated);
        Task<IEnumerable<AnnouncementDTO>> GetAnnouncementsByTodaysDateAsync();
        Task<AnnouncementDTO> GetAnnouncementByIdAsync(string id);
        Task CreateAnnouncementAsync(AnnouncementDTO announcement);
        Task UpdateAnnouncementAsync(AnnouncementDTO announcement);
        Task DeleteAnnouncementAsync(string id);
    }
}

