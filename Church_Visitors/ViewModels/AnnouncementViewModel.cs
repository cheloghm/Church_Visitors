using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using Church_Visitors.Interfaces;
using Church_Visitors.ViewModels;
using Church_Visitors.DTO;
using Church_Visitors;

namespace Church_Announcements.ViewModels
{
    public class AnnouncementsViewModel : BaseViewModel
    {
        private readonly IAnnouncementService _visitorService;
        private readonly IAlertService _alertService;
        public string Message { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public ObservableCollection<AnnouncementDTO> Announcements { get; set; }
        public ICommand GetAllAnnouncementsCommand { get; set; }
        public ICommand GetTodaysAnnouncementsCommand { get; set; }
        public ICommand GetAnnouncementsByDateCommand { get; set; }
        public ICommand ViewCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddAnnouncementCommand { get; set; }

        public AnnouncementsViewModel()
            : this(((App)Application.Current).ServiceProvider.GetService<IAnnouncementService>(),
                   ((App)Application.Current).ServiceProvider.GetService<IAlertService>())
        { }
        public AnnouncementsViewModel(IAnnouncementService visitorService, IAlertService alertService)
        {
            _visitorService = visitorService ?? throw new ArgumentNullException(nameof(visitorService));
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));

            Announcements = new ObservableCollection<AnnouncementDTO>();

            // Initialize commands
            GetAllAnnouncementsCommand = new Command(async () =>
            {
                var allAnnouncements = await _visitorService.GetAllAnnouncementsAsync();
                ClearAndPopulateAnnouncements(allAnnouncements);
            });

            GetTodaysAnnouncementsCommand = new Command(async () =>
            {
                var todaysAnnouncements = await _visitorService.GetAnnouncementsByTodaysDateAsync();
                ClearAndPopulateAnnouncements(todaysAnnouncements);
            });

            GetAnnouncementsByDateCommand = new Command<DateTime>(async (date) =>
            {
                var visitorsByDate = await _visitorService.GetAnnouncementsByDateCreatedAsync(date);
                ClearAndPopulateAnnouncements(visitorsByDate);
            });

            ViewCommand = new Command<string>(async (id) =>
            {
                // Retrieve and display the visitor details
            });

            UpdateCommand = new Command<string>(async (id) =>
            {
                // Update the visitor details
            });

            DeleteCommand = new Command<string>(async (id) =>
            {
                // Confirm and delete the visitor
            });

            AddAnnouncementCommand = new Command(async () =>
            {
                if (string.IsNullOrEmpty(Message))
                {
                    _alertService.ShowAlert("Warning", "Please fill all fields before submitting.");
                    return;
                }

                var newAnnouncement = new AnnouncementDTO
                {
                    Id = " ",
                    Message = Message,
                    DateCreated = DateTime.Now
                };

                await _visitorService.CreateAnnouncementAsync(newAnnouncement);

                // Clear the form fields for a fresh entry
                Message = string.Empty;
            });

        }

        private void ClearAndPopulateAnnouncements(IEnumerable<AnnouncementDTO> visitors)
        {
            Announcements.Clear();
            foreach (var visitor in visitors)
            {
                Announcements.Add(visitor);
            }
        }
    }
}
