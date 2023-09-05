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
    public class AnnouncementsViewModel : BaseViewModel
    {
        private readonly IAnnouncementService _announcementService;
        private IAlertService _alertService;
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public ObservableCollection<AnnouncementDTO> Announcements { get; set; }
        public ICommand GetAllAnnouncementsCommand { get; set; }
        public ICommand GetTodaysAnnouncementsCommand { get; set; }
        public ICommand GetAnnouncementsByDateCommand { get; set; }
        public ICommand AddAnnouncementCommand { get; set; }
        public ICommand PickDateAndFetchAnnouncementsCommand { get; set; }
        public ICommand ViewAnnouncementCommand { get; set; }
        public ICommand UpdateAnnouncementCommand { get; set; }
        public ICommand DeleteAnnouncementCommand { get; set; }
        public ICommand ShowAddAnnouncementFormCommand { get; set; }
        public ICommand ShowAllAnnouncementsListCommand { get; set; }
        public ICommand DateSelectedCommand { get; set; }
        public ICommand SearchAnnouncementsCommand { get; set; }

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

        private ObservableCollection<AnnouncementDTO> _fetchedAnnouncements;
        public ObservableCollection<AnnouncementDTO> FetchedAnnouncements
        {
            get => _fetchedAnnouncements;
            set => SetProperty(ref _fetchedAnnouncements, value);
        }

        public IAlertService AlertService
        {
            get => _alertService;
            set => _alertService = value;
        }

        public AnnouncementsViewModel()
            : this(((App)Application.Current).ServiceProvider.GetService<IAnnouncementService>(),
                   ((App)Application.Current).ServiceProvider.GetService<IAlertService>())
        { }
        public AnnouncementsViewModel(IAnnouncementService announcementService, IAlertService alertService)
        {
            _announcementService = announcementService ?? throw new ArgumentNullException(nameof(announcementService));
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));

            Announcements = new ObservableCollection<AnnouncementDTO>();
            FetchedAnnouncements = new ObservableCollection<AnnouncementDTO>();

            // Initialize ShowAddAnnouncementFormCommand
            ShowAddAnnouncementFormCommand = new Command(() =>
            {
                IsFormVisible = true;
                IsDatePickerVisible = false;
                FetchedAnnouncements.Clear(); // Clear the fetched announcements when showing the form
            });

            // Initialize ShowAllAnnouncementsListCommand
            ShowAllAnnouncementsListCommand = new Command(async () =>
            {
                IsFormVisible = false;
                IsDatePickerVisible = false;
                await LoadAllAnnouncements(); // Fetch and display announcements when showing the list
            });

            // Initialize commands
            GetAllAnnouncementsCommand = new Command(async () =>
            {
                var allAnnouncements = await _announcementService.GetAllAnnouncementsAsync();
                ClearAndPopulateAnnouncements(allAnnouncements);
            });

            GetTodaysAnnouncementsCommand = new Command(async () =>
            {
                var todaysAnnouncements = await _announcementService.GetAnnouncementsByTodaysDateAsync();
                ClearAndPopulateAnnouncements(todaysAnnouncements);
                IsFormVisible = false;
                IsDatePickerVisible = false;
            });

            GetAnnouncementsByDateCommand = new Command<DateTime>(async (date) =>
            {
                IsDatePickerVisible = true;
                IsFormVisible = false;
                var announcementsByDate = await _announcementService.GetAnnouncementsByDateCreatedAsync(date);
                ClearAndPopulateAnnouncements(announcementsByDate);
            });

            // Inside AnnouncementsViewModel constructor
            ViewAnnouncementCommand = new Command<AnnouncementDTO>(async (announcement) =>
            {
                // Show announcement details in a modal dialog
                await _alertService.ShowAnnouncementDetailsAsync(announcement);
            });

            // Initialize UpdateAnnouncementCommand
            UpdateAnnouncementCommand = new Command<AnnouncementDTO>(async (announcement) =>
            {
                // Show update modal dialog pre-populated with announcement details
                await _alertService.ShowUpdateAnnouncementAsync(announcement, this);
            });

            // Initialize DeleteAnnouncementCommand
            DeleteAnnouncementCommand = new Command<AnnouncementDTO>(async (announcement) =>
            {
                bool shouldDelete = await _alertService.ShowConfirmationAlertAsync("Delete Announcement", "Are you sure you want to delete this announcement?");
                if (shouldDelete)
                {
                    // Delete announcement
                    // ...
                }
            });

            SearchAnnouncementsCommand = new Command<string>(async (query) =>
            {
                var searchedAnnouncements = await _announcementService.SearchAnnouncementsAsync(query);
                ClearAndPopulateAnnouncements(searchedAnnouncements);
            });

            AddAnnouncementCommand = new Command(async () =>
            {
                if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Message))
                {
                    _alertService.ShowAlert("Warning", "Please fill all fields before submitting.");
                    return;
                }

                var newAnnouncement = new AnnouncementDTO
                {
                    Id = " ",
                    Title = Title,
                    Message = Message,
                    DateCreated = DateTime.Now.ToString()
                };

                await _announcementService.CreateAnnouncementAsync(newAnnouncement);

                // Clear the form fields for a fresh entry
                Title = string.Empty;
                Message = string.Empty;
            });

            DateSelectedCommand = new Command<DateTime>((selectedDate) =>
            {
                FetchAnnouncementsBySelectedDate(selectedDate);
            });

            // Inside your constructor or initialization code
            PickDateAndFetchAnnouncementsCommand = new Command(() =>
            {
                IsDatePickerVisible = !IsDatePickerVisible;
            });

        }

        private async Task LoadAllAnnouncements()
        {
            var allAnnouncements = await _announcementService.GetAllAnnouncementsAsync();
            FetchedAnnouncements.Clear();
            foreach (var announcement in allAnnouncements)
            {
                FetchedAnnouncements.Add(announcement);
            }
        }

        private void ClearAndPopulateAnnouncements(IEnumerable<AnnouncementDTO> announcements)
        {
            Announcements.Clear();
            FetchedAnnouncements.Clear();

            foreach (var announcement in announcements)
            {
                Announcements.Add(announcement);
                FetchedAnnouncements.Add(announcement);
            }
        }

        public void UpdateAnnouncement(AnnouncementDTO updatedAnnouncement)
        {
            // Find the announcement in the collection and update its properties
            var announcementToUpdate = Announcements.FirstOrDefault(v => v.Id == updatedAnnouncement.Id);
            if (announcementToUpdate != null)
            {
                announcementToUpdate.Title = updatedAnnouncement.Title;
                announcementToUpdate.Message = updatedAnnouncement.Message;

                // Notify UI that the properties have changed
                OnPropertyChanged(nameof(Announcements));
            }
        }

        public async Task DeleteAnnouncementAsync(AnnouncementDTO announcementToDelete)
        {
            await _announcementService.DeleteAnnouncementAsync(announcementToDelete.Id);
            // Refresh the announcement list after delete
            await LoadAllAnnouncements();
        }

        public async void FetchAnnouncementsBySelectedDate(DateTime selectedDate)
        {
            var announcementsByDate = await _announcementService.GetAnnouncementsByDateCreatedAsync(selectedDate);
            ClearAndPopulateAnnouncements(announcementsByDate);
        }

    }
}
