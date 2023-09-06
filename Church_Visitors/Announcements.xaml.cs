using Church_Visitors;
using Church_Visitors.DTO;
using Church_Visitors.Interfaces;
using Church_Visitors.ViewModels;

namespace Church_Visitors;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Announcements : ContentPage
{
    public Announcements()
    {
        InitializeComponent();
        BindingContext = ((App)Application.Current).ServiceProvider.GetService<AnnouncementsViewModel>();
        // Inject IAlertService into AnnouncementsViewModel here (if not done already)
        var viewModel = new AnnouncementsViewModel(
    ((App)Application.Current).ServiceProvider.GetService<IAnnouncementService>(),
    ((App)Application.Current).ServiceProvider.GetService<IAlertService>()
);
        BindingContext = viewModel;
    }

    private string _searchQuery;

    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            _searchQuery = value;
            ((AnnouncementsViewModel)BindingContext).SearchAnnouncementsCommand.Execute(_searchQuery);
        }
    }

    // TextChanged event handler
    void OnSearchTextChanged(object sender, TextChangedEventArgs args)
    {
        SearchQuery = args.NewTextValue;
    }


    private async void ViewAnnouncementClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var announcement = button?.CommandParameter as AnnouncementDTO;
        await ((AnnouncementsViewModel)BindingContext).AlertService.ShowAnnouncementDetailsAsync(announcement);
    }

    private async void UpdateAnnouncementClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var visitor = button?.CommandParameter as AnnouncementDTO;
        await ((AnnouncementsViewModel)BindingContext).AlertService.ShowUpdateAnnouncementAsync(visitor, (AnnouncementsViewModel)BindingContext);
    }

    private async void DeleteAnnouncementClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var visitor = button?.CommandParameter as AnnouncementDTO;
        var shouldDelete = await ((AnnouncementsViewModel)BindingContext).AlertService.ShowConfirmationAlertAsync("Delete Announcement", "Are you sure you want to delete this visitor?");
        if (shouldDelete)
        {
            await ((AnnouncementsViewModel)BindingContext).DeleteAnnouncementAsync(visitor);
        }
    }

    private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        var viewModel = BindingContext as AnnouncementsViewModel;
        if (viewModel != null)
        {
            var selectedDate = e.NewDate;
            viewModel.FetchAnnouncementsBySelectedDate(selectedDate);
        }
    }

    private void ClearAnnouncementClicked(object sender, EventArgs e)
    {
        // Access the Entry elements by their names
        TitleEntry.Text = string.Empty;
        MessageEntry.Text = string.Empty;

        // Optionally, you can hide the form if needed
        //viewModel.IsFormVisible = false;
    }
}