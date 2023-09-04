using Church_Visitors.Interfaces;
using Church_Visitors.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Church_Visitors.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementService(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task<IEnumerable<AnnouncementDTO>> GetAllAnnouncementsAsync() =>
            await _announcementRepository.GetAllAnnouncementsAsync();

        public async Task<IEnumerable<AnnouncementDTO>> GetAnnouncementsByDateCreatedAsync(DateTime dateCreated) =>
            await _announcementRepository.GetAnnouncementsByDateCreatedAsync(dateCreated);

        public async Task<IEnumerable<AnnouncementDTO>> GetAnnouncementsByTodaysDateAsync() =>
            await _announcementRepository.GetAnnouncementsByTodaysDateAsync();

        public async Task<AnnouncementDTO> GetAnnouncementByIdAsync(string id) =>
            await _announcementRepository.GetAnnouncementByIdAsync(id);

        public async Task CreateAnnouncementAsync(AnnouncementDTO announcement) =>
            await _announcementRepository.CreateAnnouncementAsync(announcement);

        public async Task UpdateAnnouncementAsync(AnnouncementDTO announcement) =>
            await _announcementRepository.UpdateAnnouncementAsync(announcement);

        public async Task DeleteAnnouncementAsync(string id) =>
            await _announcementRepository.DeleteAnnouncementAsync(id);

        public async Task<IEnumerable<AnnouncementDTO>> SearchAnnouncementsAsync(string searchText) =>
            await _announcementRepository.SearchAnnouncementsAsync(searchText);
    }
}
