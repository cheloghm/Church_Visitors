using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church_Visitors.Interfaces
{
    public interface IAlertService
    {
        void ShowAlert(string title, string message);
    }
}

