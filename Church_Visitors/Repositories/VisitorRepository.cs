using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Church_Visitors.DTO;
using Church_Visitors.Interfaces;
using System.Text;

namespace Church_Visitors.Repositories
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly HttpClient _httpClient;

        public VisitorRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<VisitorDTO>> GetAllVisitorsAsync()
        {
            var response = await _httpClient.GetStringAsync("visitor");
            return JsonConvert.DeserializeObject<IEnumerable<VisitorDTO>>(response);
        }

        public async Task<IEnumerable<VisitorDTO>> GetVisitorsByDateEnteredAsync(DateTime dateEntered)
        {
            var response = await _httpClient.GetStringAsync($"/visitors/date/{dateEntered.ToString("yyyy-MM-dd")}");
            return JsonConvert.DeserializeObject<IEnumerable<VisitorDTO>>(response);
        }

        public async Task<IEnumerable<VisitorDTO>> GetVisitorsByTodaysDateAsync()
        {
            var today = DateTime.Today;
            var response = await _httpClient.GetStringAsync($"/visitors/date/{today.ToString("yyyy-MM-dd")}");
            return JsonConvert.DeserializeObject<IEnumerable<VisitorDTO>>(response);
        }

        public async Task<VisitorDTO> GetVisitorByIdAsync(string id)
        {
            var response = await _httpClient.GetStringAsync($"/visitors/{id}");
            return JsonConvert.DeserializeObject<VisitorDTO>(response);
        }

        public async Task CreateVisitorAsync(VisitorDTO visitor)
        {
            var content = new StringContent(JsonConvert.SerializeObject(visitor), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("visitor", content);
        }

        public async Task UpdateVisitorAsync(VisitorDTO visitor)
        {
            var content = new StringContent(JsonConvert.SerializeObject(visitor), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"/visitors/{visitor.Id}", content);
        }

        public async Task DeleteVisitorAsync(string id)
        {
            await _httpClient.DeleteAsync($"/visitors/{id}");
        }

        public async Task<IEnumerable<VisitorDTO>> SearchVisitorsAsync(string searchText)
        {
            var response = await _httpClient.GetStringAsync($"/visitors/search/{searchText}");
            return JsonConvert.DeserializeObject<IEnumerable<VisitorDTO>>(response);
        }
    }
}
