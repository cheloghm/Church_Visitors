using System.Collections.Generic;
using System.Threading.Tasks;
using Church_Visitors.Interfaces;
using Church_Visitors.Models;
using Church_Visitors.Data;
using MongoDB.Driver;

namespace Church_Visitors.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IMongoCollection<Announcement> _announcements;

        public AnnouncementService(DataContext dataContext, string collectionName)
        {
            _announcements = dataContext.GetCollection<Announcement>(collectionName);
        }

        public async Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync() =>
            await _announcements.Find(announcement => true).ToListAsync();

        public async Task<IEnumerable<Announcement>> GetAnnouncementsByDateCreatedAsync(DateTime dateCreated) =>
            await _announcements.Find(announcement => announcement.DateCreated.Date == dateCreated.Date).ToListAsync();

        public async Task<IEnumerable<Announcement>> GetAnnouncementsByTodaysDateAsync()
        {
            var today = DateTime.Today;
            return await _announcements.Find(announcement => announcement.DateCreated.Date == today).ToListAsync();
        }

        public async Task<Announcement> GetAnnouncementByIdAsync(string id) =>
            await _announcements.Find(announcement => announcement.Id == id).FirstOrDefaultAsync();

        public async Task CreateAnnouncementAsync(Announcement announcement) =>
            await _announcements.InsertOneAsync(announcement);

        public async Task UpdateAnnouncementAsync(Announcement announcement) =>
            await _announcements.ReplaceOneAsync(a => a.Id == announcement.Id, announcement);

        public async Task DeleteAnnouncementAsync(string id) =>
            await _announcements.DeleteOneAsync(a => a.Id == id);
    }
}
