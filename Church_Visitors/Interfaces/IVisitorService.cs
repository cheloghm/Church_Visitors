using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Church_Visitors.Models;

namespace Church_Visitors.Services
{
    public interface IVisitorService
    {
        Task<IEnumerable<Visitor>> GetAllVisitorsAsync();
        Task<IEnumerable<Visitor>> GetVisitorsByDateEnteredAsync(DateTime dateEntered);
        Task<IEnumerable<Visitor>> GetVisitorsByTodaysDateAsync();
        Task<Visitor> GetVisitorByIdAsync(string id);
        Task CreateVisitorAsync(Visitor visitor);
        Task UpdateVisitorAsync(Visitor visitor);
        Task DeleteVisitorAsync(string id);
        Task<IEnumerable<Visitor>> SearchVisitorsAsync(string searchText);
    }
}
