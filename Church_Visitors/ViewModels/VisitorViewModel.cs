using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Church_Visitors.Interfaces;
using Church_Visitors.DTO;
using Church_Visitors.Services;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace Church_Visitors.ViewModels
{
    public class VisitorsViewModel : BaseViewModel
    {
        private readonly IVisitorService _visitorService;
        private readonly IAlertService _alertService;
        public string FullName { get; set; }
        public string GuestOf { get; set; }
        public DateTime DateVisited { get; set; } = DateTime.Now;
        public string OtherRemarks { get; set; }
        public ObservableCollection<VisitorDTO> Visitors { get; set; }
        public ICommand GetAllVisitorsCommand { get; set; }
        public ICommand GetTodaysVisitorsCommand { get; set; }
        public ICommand GetVisitorsByDateCommand { get; set; }
        public ICommand ViewCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddVisitorCommand { get; set; }

        private bool _isFormVisible = false;
        public bool IsFormVisible
        {
            get => _isFormVisible;
            set => SetProperty(ref _isFormVisible, value);
        }

        private ObservableCollection<VisitorDTO> _fetchedVisitors;
        public ObservableCollection<VisitorDTO> FetchedVisitors
        {
            get => _fetchedVisitors;
            set => SetProperty(ref _fetchedVisitors, value);
        }

        public ICommand ViewVisitorCommand { get; set; }
        public ICommand UpdateVisitorCommand { get; set; }
        public ICommand DeleteVisitorCommand { get; set; }
        public ICommand ShowAddVisitorFormCommand { get; set; }
        public ICommand ShowAllVisitorsListCommand { get; set; }

        public VisitorsViewModel()
            : this(((App)Application.Current).ServiceProvider.GetService<IVisitorService>(),
                   ((App)Application.Current).ServiceProvider.GetService<IAlertService>())
        { }
        public VisitorsViewModel(IVisitorService visitorService, IAlertService alertService)
        {
            _visitorService = visitorService ?? throw new ArgumentNullException(nameof(visitorService));
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));

            Visitors = new ObservableCollection<VisitorDTO>();
            FetchedVisitors = new ObservableCollection<VisitorDTO>();

            // Initialize ShowAddVisitorFormCommand
            ShowAddVisitorFormCommand = new Command(() =>
            {
                IsFormVisible = true;
                FetchedVisitors.Clear(); // Clear the fetched visitors when showing the form
            });

            // Initialize ShowAllVisitorsListCommand
            ShowAllVisitorsListCommand = new Command(async () =>
            {
                IsFormVisible = false;
                await LoadAllVisitors(); // Fetch and display visitors when showing the list
            });

            // Initialize commands
            GetAllVisitorsCommand = new Command(async () =>
            {
                var allVisitors = await _visitorService.GetAllVisitorsAsync();
                ClearAndPopulateVisitors(allVisitors);
            });

            GetTodaysVisitorsCommand = new Command(async () =>
            {
                var todaysVisitors = await _visitorService.GetVisitorsByTodaysDateAsync();
                ClearAndPopulateVisitors(todaysVisitors);
            });

            GetVisitorsByDateCommand = new Command<DateTime>(async (date) =>
            {
                var visitorsByDate = await _visitorService.GetVisitorsByDateEnteredAsync(date);
                ClearAndPopulateVisitors(visitorsByDate);
            });

            // Inside VisitorsViewModel constructor
            ViewVisitorCommand = new Command<VisitorDTO>(async (visitor) =>
            {
                // Use the visitor parameter to show the details or navigate to a details page
            });

            UpdateVisitorCommand = new Command<VisitorDTO>(async (visitor) =>
            {
                // Use the visitor parameter to initiate an update process
            });

            DeleteVisitorCommand = new Command<VisitorDTO>(async (visitor) =>
            {
                // Use the visitor parameter to initiate a delete process
            });

            AddVisitorCommand = new Command(async () =>
            {
                if (string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(OtherRemarks))
                {
                    _alertService.ShowAlert("Warning", "Please fill all fields before submitting.");
                    return;
                }

                var newVisitor = new VisitorDTO
                {
                    Id = " ",
                    FullName = FullName,
                    GuestOf = GuestOf,
                    DateEntered = DateTime.Now,
                    OtherRemarks = OtherRemarks
                };

                await _visitorService.CreateVisitorAsync(newVisitor);

                // Clear the form fields for a fresh entry
                FullName = string.Empty;
                GuestOf = string.Empty;
                OtherRemarks = string.Empty;
            });

        }

        private async Task LoadAllVisitors()
        {
            var allVisitors = await _visitorService.GetAllVisitorsAsync();
            FetchedVisitors.Clear();
            foreach (var visitor in allVisitors)
            {
                FetchedVisitors.Add(visitor);
            }
        }

        private void ClearAndPopulateVisitors(IEnumerable<VisitorDTO> visitors)
        {
            Visitors.Clear();
            foreach (var visitor in visitors)
            {
                Visitors.Add(visitor);
            }
        }
    }
}
