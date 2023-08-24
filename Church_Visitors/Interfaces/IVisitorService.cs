using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Church_Visitors.Models;
using Church_Visitors.DTO;

namespace Church_Visitors.Services
{
    public interface IVisitorService
    {
        Task<IEnumerable<VisitorDTO>> GetAllVisitorsAsync();
        Task<IEnumerable<VisitorDTO>> GetVisitorsByDateEnteredAsync(DateTime dateEntered);
        Task<IEnumerable<VisitorDTO>> GetVisitorsByTodaysDateAsync();
        Task<VisitorDTO> GetVisitorByIdAsync(string id);
        Task CreateVisitorAsync(VisitorDTO visitor);
        Task UpdateVisitorAsync(VisitorDTO visitor);
        Task DeleteVisitorAsync(string id);
        Task<IEnumerable<VisitorDTO>> SearchVisitorsAsync(string searchText);
    }
}
