using System.Collections.ObjectModel;
using System.Windows.Input;
using Church_Visitors.Models;
using Church_Visitors.Services;
using Microsoft.Maui.Controls;

namespace Church_Visitors.ViewModels
{
    public class VisitorViewModel : BaseViewModel
    {
        private readonly IVisitorService _visitorService;

        public VisitorViewModel(IVisitorService visitorService)
        {
            _visitorService = visitorService;

            GetAllVisitorsCommand = new Command(async () =>
            {
                // Load all visitors
                var visitors = await _visitorService.GetAllVisitorsAsync();
                Visitors = new ObservableCollection<Visitor>(visitors);
            });

            // TODO: Add other commands here (e.g., GetToday'sVisitors, GetVisitorsByDate, etc.)
        }

        public ObservableCollection<Visitor> Visitors { get; private set; }

        public ICommand GetAllVisitorsCommand { get; }

        // TODO: Define other commands (e.g., GetToday'sVisitors, GetVisitorsByDate, etc.)
    }
}
