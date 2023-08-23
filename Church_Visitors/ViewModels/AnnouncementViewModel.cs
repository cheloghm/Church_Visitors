using Church_Visitors.Interfaces;
using Church_Visitors.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Church_Visitors.ViewModels
{
    public class AnnouncementViewModel : BaseViewModel
    {
        private readonly IAnnouncementService _announcementService;

        public ObservableCollection<AnnouncementDTO> Announcements { get; }

        public AnnouncementViewModel(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
            Announcements = new ObservableCollection<AnnouncementDTO>();
        }

        public async Task LoadAnnouncementsAsync()
        {
            var announcements = await _announcementService.GetAllAnnouncementsAsync();
            Announcements.Clear();
            foreach (var announcement in announcements)
            {
                Announcements.Add(announcement);
            }
        }
    }
}
