using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Church_Visitors.DTO;
using Church_Visitors.Interfaces;
using System.Text;

namespace Church_Visitors.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly HttpClient _httpClient;

        public AnnouncementRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<AnnouncementDTO>> GetAllAnnouncementsAsync()
        {
            var response = await _httpClient.GetStringAsync("Announcements");
            return JsonConvert.DeserializeObject<IEnumerable<AnnouncementDTO>>(response);
        }

        public async Task<IEnumerable<AnnouncementDTO>> GetAnnouncementsByDateCreatedAsync(DateTime dateCreated)
        {
            var response = await _httpClient.GetStringAsync($"Announcement/by-date?date={dateCreated.ToString("yyyy-MM-dd")}");
            return JsonConvert.DeserializeObject<IEnumerable<AnnouncementDTO>>(response);
        }

        public async Task<IEnumerable<AnnouncementDTO>> GetAnnouncementsByTodaysDateAsync()
        {
            var today = DateTime.Today.Date;
            var queryParam = today.ToString("yyyy-MM-dd");
            var response = await _httpClient.GetStringAsync($"Announcement/by-date?date={queryParam}");
            return JsonConvert.DeserializeObject<IEnumerable<AnnouncementDTO>>(response);
        }

        public async Task<AnnouncementDTO> GetAnnouncementByIdAsync(string id)
        {
            var response = await _httpClient.GetStringAsync($"Announcements/{id}");
            return JsonConvert.DeserializeObject<AnnouncementDTO>(response);
        }

        public async Task CreateAnnouncementAsync(AnnouncementDTO announcement)
        {
            var content = new StringContent(JsonConvert.SerializeObject(announcement), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("Announcements", content);
        }

        public async Task UpdateAnnouncementAsync(AnnouncementDTO announcement)
        {
            var content = new StringContent(JsonConvert.SerializeObject(announcement), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"Announcements/{announcement.Id}", content);
        }

        public async Task DeleteAnnouncementAsync(string id)
        {
            await _httpClient.DeleteAsync($"Announcements/{id}");
        }

        public async Task<IEnumerable<VisitorDTO>> SearchAnnouncementsAsync(string searchText)
        {
            var encodedSearchText = Uri.EscapeDataString(searchText);
            var response = await _httpClient.GetStringAsync($"Announcement/search?query={encodedSearchText}");
            return JsonConvert.DeserializeObject<IEnumerable<VisitorDTO>>(response);
        }
    }
}
