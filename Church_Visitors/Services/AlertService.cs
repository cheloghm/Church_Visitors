using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Church_Visitors.Interfaces;
using Microsoft.Maui.Controls;

namespace Church_Visitors.Services
{
    public class AlertService : IAlertService
    {
        public void ShowAlert(string title, string message)
        {
            Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }
    }
}

