﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Church_Visitors.Interfaces;
using Church_Visitors.Models;
using Church_Visitors.Services;
using Microsoft.Maui.Controls;

namespace Church_Visitors.ViewModels
{
    public class VisitorsViewModel : BaseViewModel
    {
        private readonly IVisitorService _visitorService;
        private readonly IAlertService _alertService;
        // Properties for the form
        public string FullName { get; set; }
        public DateTime DateVisited { get; set; } = DateTime.Now; // default to today's date
        public string OtherRemarks { get; set; }
        public ObservableCollection<VisitorDTO> Visitors { get; set; }
        public ICommand GetAllVisitorsCommand { get; set; }
        public ICommand GetTodaysVisitorsCommand { get; set; }
        public ICommand GetVisitorsByDateCommand { get; set; }
        public ICommand ViewCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddVisitorCommand { get; set; }

        public VisitorsViewModel(IVisitorService visitorService, IAlertService alertService)
        {
            _visitorService = visitorService ?? throw new ArgumentNullException(nameof(visitorService));
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));

            Visitors = new ObservableCollection<VisitorDTO>();

            // Initialize commands
            GetAllVisitorsCommand = new Command(async () =>
            {
                var allVisitors = await _visitorService.GetAllVisitorsAsync();
                foreach (var visitor in allVisitors)
                {
                    Visitors.Add(visitor);
                }
            });

            GetTodaysVisitorsCommand = new Command(async () =>
            {
                var todaysVisitors = await _visitorService.GetVisitorsByTodaysDateAsync();
                foreach (var visitor in todaysVisitors)
                {
                    Visitors.Add(visitor);
                }
            });

            GetVisitorsByDateCommand = new Command<DateTime>(async (date) =>
            {
                var visitorsByDate = await _visitorService.GetVisitorsByDateEnteredAsync(date);
                foreach (var visitor in visitorsByDate)
                {
                    Visitors.Add(visitor);
                }
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

            AddVisitorCommand = new Command(async () =>
            {
                if (string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(OtherRemarks))
                {
                    _alertService.ShowAlert("Warning", "Please fill all fields before submitting.");
                    return;
                }

                var newVisitor = new VisitorDTO
                {
                    FullName = FullName,
                    DateEntered = DateVisited,
                    OtherRemarks = OtherRemarks
                };

                await _visitorService.CreateVisitorAsync(newVisitor);

                // Clear the form fields for a fresh entry
                FullName = string.Empty;
                DateVisited = DateTime.Now;
                OtherRemarks = string.Empty;
            });

        }
    }
}
