using Church_Visitors.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Church_Visitors.Interfaces
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync();
        Task<IEnumerable<Announcement>> GetAnnouncementsByDateCreatedAsync(DateTime dateCreated);
        Task<IEnumerable<Announcement>> GetAnnouncementsByTodaysDateAsync();
        Task<Announcement> GetAnnouncementByIdAsync(string id);
        Task CreateAnnouncementAsync(Announcement announcement);
        Task UpdateAnnouncementAsync(Announcement announcement);
        Task DeleteAnnouncementAsync(string id);
    }
}
