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
            var response = await _httpClient.GetStringAsync("/announcements");
            return JsonConvert.DeserializeObject<IEnumerable<AnnouncementDTO>>(response);
        }

        public async Task<IEnumerable<AnnouncementDTO>> GetAnnouncementsByDateCreatedAsync(DateTime dateCreated)
        {
            var response = await _httpClient.GetStringAsync($"/announcements/date/{dateCreated.ToString("yyyy-MM-dd")}");
            return JsonConvert.DeserializeObject<IEnumerable<AnnouncementDTO>>(response);
        }

        public async Task<IEnumerable<AnnouncementDTO>> GetAnnouncementsByTodaysDateAsync()
        {
            var today = DateTime.Today;
            var response = await _httpClient.GetStringAsync($"/announcements/date/{today.ToString("yyyy-MM-dd")}");
            return JsonConvert.DeserializeObject<IEnumerable<AnnouncementDTO>>(response);
        }

        public async Task<AnnouncementDTO> GetAnnouncementByIdAsync(string id)
        {
            var response = await _httpClient.GetStringAsync($"/announcements/{id}");
            return JsonConvert.DeserializeObject<AnnouncementDTO>(response);
        }

        public async Task CreateAnnouncementAsync(AnnouncementDTO announcement)
        {
            var content = new StringContent(JsonConvert.SerializeObject(announcement), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("/announcements", content);
        }

        public async Task UpdateAnnouncementAsync(AnnouncementDTO announcement)
        {
            var content = new StringContent(JsonConvert.SerializeObject(announcement), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"/announcements/{announcement.Id}", content);
        }

        public async Task DeleteAnnouncementAsync(string id)
        {
            await _httpClient.DeleteAsync($"/announcements/{id}");
        }
    }
}
