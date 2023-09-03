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
        private IAlertService _alertService;
        public string FullName { get; set; }
        public string GuestOf { get; set; }
        public DateTime DateVisited { get; set; } = DateTime.Now;
        public string OtherRemarks { get; set; }
        public ObservableCollection<VisitorDTO> Visitors { get; set; }
        public ICommand GetAllVisitorsCommand { get; set; }
        public ICommand GetTodaysVisitorsCommand { get; set; }
        public ICommand GetVisitorsByDateCommand { get; set; }
        public ICommand AddVisitorCommand { get; set; }
        public ICommand PickDateAndFetchVisitorsCommand { get; set; }
        public ICommand ViewVisitorCommand { get; set; }
        public ICommand UpdateVisitorCommand { get; set; }
        public ICommand DeleteVisitorCommand { get; set; }
        public ICommand ShowAddVisitorFormCommand { get; set; }
        public ICommand ShowAllVisitorsListCommand { get; set; }
        public ICommand DateSelectedCommand { get; set; }
        public ICommand SearchVisitorsCommand { get; set; }

        private bool _isFormVisible = false;
        private DateTime _selectedDate = DateTime.Now;
        private bool _isDatePickerVisible;

        public bool IsFormVisible
        {
            get => _isFormVisible;
            set => SetProperty(ref _isFormVisible, value);
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        public bool IsDatePickerVisible
        {
            get => _isDatePickerVisible;
            set => SetProperty(ref _isDatePickerVisible, value);
        }

        private ObservableCollection<VisitorDTO> _fetchedVisitors;
        public ObservableCollection<VisitorDTO> FetchedVisitors
        {
            get => _fetchedVisitors;
            set => SetProperty(ref _fetchedVisitors, value);
        }

        public IAlertService AlertService
        {
            get => _alertService;
            set => _alertService = value;
        }

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
                IsFormVisible = false; 
            });

            GetVisitorsByDateCommand = new Command<DateTime>(async (date) =>
            {
                var visitorsByDate = await _visitorService.GetVisitorsByDateEnteredAsync(date);
                ClearAndPopulateVisitors(visitorsByDate);
            });

            // Inside VisitorsViewModel constructor
            ViewVisitorCommand = new Command<VisitorDTO>(async (visitor) =>
            {
                // Show visitor details in a modal dialog
                await _alertService.ShowVisitorDetailsAsync(visitor);
            });

            // Initialize UpdateVisitorCommand
            UpdateVisitorCommand = new Command<VisitorDTO>(async (visitor) =>
            {
                // Show update modal dialog pre-populated with visitor details
                await _alertService.ShowUpdateVisitorAsync(visitor, this);
            });

            // Initialize DeleteVisitorCommand
            DeleteVisitorCommand = new Command<VisitorDTO>(async (visitor) =>
            {
                bool shouldDelete = await _alertService.ShowConfirmationAlertAsync("Delete Visitor", "Are you sure you want to delete this visitor?");
                if (shouldDelete)
                {
                    // Delete visitor
                    // ...
                }
            });

            SearchVisitorsCommand = new Command<string>(async (query) =>
            {
                var searchedVisitors = await _visitorService.SearchVisitorsAsync(query);
                ClearAndPopulateVisitors(searchedVisitors);
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

            DateSelectedCommand = new Command<DateTime>((selectedDate) =>
            {
                FetchVisitorsBySelectedDate(selectedDate);
            });

            // Inside your constructor or initialization code
            PickDateAndFetchVisitorsCommand = new Command(() =>
            {
                IsDatePickerVisible = !IsDatePickerVisible;
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
            FetchedVisitors.Clear();

            foreach (var visitor in visitors)
            {
                Visitors.Add(visitor);
                FetchedVisitors.Add(visitor);
            }
        }

        public void UpdateVisitor(VisitorDTO updatedVisitor)
        {
            // Find the visitor in the collection and update its properties
            var visitorToUpdate = Visitors.FirstOrDefault(v => v.Id == updatedVisitor.Id);
            if (visitorToUpdate != null)
            {
                visitorToUpdate.FullName = updatedVisitor.FullName;
                visitorToUpdate.GuestOf = updatedVisitor.GuestOf;
                visitorToUpdate.OtherRemarks = updatedVisitor.OtherRemarks;

                // Notify UI that the properties have changed
                OnPropertyChanged(nameof(Visitors));
            }
        }

        public async Task DeleteVisitorAsync(VisitorDTO visitorToDelete)
        {
            await _visitorService.DeleteVisitorAsync(visitorToDelete.Id);
            // Refresh the visitor list after delete
            await LoadAllVisitors();
        }

        public async void FetchVisitorsBySelectedDate(DateTime selectedDate)
        {
            var visitorsByDate = await _visitorService.GetVisitorsByDateEnteredAsync(selectedDate);
            ClearAndPopulateVisitors(visitorsByDate);
        }

    }
}
