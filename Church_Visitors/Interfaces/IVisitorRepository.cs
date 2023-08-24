using Church_Visitors.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church_Visitors.Interfaces
{
    public interface IVisitorRepository
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
