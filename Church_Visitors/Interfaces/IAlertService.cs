using Church_Visitors.DTO;
using Church_Visitors.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church_Visitors.Interfaces
{
    public interface IAlertService
    {
        Task ShowAlert(string title, string message);
        Task ShowVisitorDetailsAsync(VisitorDTO visitor);
        Task ShowUpdateVisitorAsync(VisitorDTO visitor, VisitorsViewModel viewModel);
        Task<bool> ShowConfirmationAlertAsync(string title, string message);
    }
}

