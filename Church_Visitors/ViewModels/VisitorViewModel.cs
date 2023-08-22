using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Church_Visitors.Models;
using Church_Visitors.Services;
using IntelliJ.Lang.Annotations;
using Microsoft.Maui.Controls;

namespace Church_Visitors.ViewModels
{
    public class VisitorViewModel : BaseViewModel
    {
        private readonly IVisitorService _visitorService;

        public ObservableCollection<Visitor> Visitors { get; } = new ObservableCollection<Visitor>();

        public Command LoadVisitorsCommand { get; }

        public VisitorViewModel(IVisitorService visitorService)
        {
            _visitorService = visitorService;
            LoadVisitorsCommand = new Command(async () => await LoadVisitorsAsync());
        }

        private async Task LoadVisitorsAsync()
        {
            IsBusy = true;

            try
            {
                var visitors = await _visitorService.GetAllVisitorsAsync();
                Visitors.Clear();

                foreach (var visitor in visitors)
                {
                    Visitors.Add(visitor);
                }
            }
            catch (Exception ex)
            {
                // Handle the error
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
