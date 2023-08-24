using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Church_Visitors.Interfaces;
using Church_Visitors.Models;
using Church_Visitors.DTO;
using Church_Visitors.Repositories;

namespace Church_Visitors.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IVisitorRepository _visitorRepository;

        public VisitorService(IVisitorRepository visitorRepository)
        {
            _visitorRepository = visitorRepository;
        }

        public async Task<IEnumerable<VisitorDTO>> GetAllVisitorsAsync() =>
            await _visitorRepository.GetAllVisitorsAsync();

        public async Task<IEnumerable<VisitorDTO>> GetVisitorsByDateEnteredAsync(DateTime dateEntered) =>
            await _visitorRepository.GetVisitorsByDateEnteredAsync(dateEntered);

        public async Task<IEnumerable<VisitorDTO>> GetVisitorsByTodaysDateAsync() =>
            await _visitorRepository.GetVisitorsByTodaysDateAsync();

        public async Task<VisitorDTO> GetVisitorByIdAsync(string id) =>
            await _visitorRepository.GetVisitorByIdAsync(id);

        public async Task CreateVisitorAsync(VisitorDTO visitor) =>
            await _visitorRepository.CreateVisitorAsync(visitor);

        public async Task UpdateVisitorAsync(VisitorDTO visitor) =>
            await _visitorRepository.UpdateVisitorAsync(visitor);

        public async Task DeleteVisitorAsync(string id) =>
            await _visitorRepository.DeleteVisitorAsync(id);

        public async Task<IEnumerable<VisitorDTO>> SearchVisitorsAsync(string searchText) =>
            await _visitorRepository.SearchVisitorsAsync(searchText);
    }
}
