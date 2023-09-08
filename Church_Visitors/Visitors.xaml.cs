using Church_Visitors.DTO;
using Church_Visitors.Interfaces;
using Church_Visitors.Services;
using Church_Visitors.ViewModels;

namespace Church_Visitors;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Visitors : ContentPage
{
    public Visitors()
    {
        InitializeComponent();
        BindingContext = ((App)Application.Current).ServiceProvider.GetService<VisitorsViewModel>();
        // Inject IAlertService into VisitorsViewModel here (if not done already)

    }

    private string _searchQuery;

    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            _searchQuery = value;
            ((VisitorsViewModel)BindingContext).SearchVisitorsCommand.Execute(_searchQuery);
        }
    }

    // TextChanged event handler
    void OnSearchTextChanged(object sender, TextChangedEventArgs args)
    {
        SearchQuery = args.NewTextValue;
    }


    private async void ViewVisitorClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var visitor = button?.CommandParameter as VisitorDTO;
        await ((VisitorsViewModel)BindingContext).AlertService.ShowVisitorDetailsAsync(visitor);
    }

    private async void UpdateVisitorClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var visitor = button?.CommandParameter as VisitorDTO;
        await ((VisitorsViewModel)BindingContext).AlertService.ShowUpdateVisitorAsync(visitor, (VisitorsViewModel)BindingContext);
    }

    private async void DeleteVisitorClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var visitor = button?.CommandParameter as VisitorDTO;
        var shouldDelete = await ((VisitorsViewModel)BindingContext).AlertService.ShowConfirmationAlertAsync("Delete Visitor", "Are you sure you want to delete this visitor?");
        if (shouldDelete)
        {
            await ((VisitorsViewModel)BindingContext).DeleteVisitorAsync(visitor);
        }
    }

    private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        var viewModel = BindingContext as VisitorsViewModel;
        if (viewModel != null)
        {
            var selectedDate = e.NewDate;
            viewModel.FetchVisitorsBySelectedDate(selectedDate);
        }
    }

    private void ClearVisitorClicked(object sender, EventArgs e)
    {
        // Access the Entry elements by their names
        FullNameEntry.Text = string.Empty;
        GuestOfEntry.Text = string.Empty;
        OtherRemarksEntry.Text = string.Empty;

        // Optionally, you can hide the form if needed
        //viewModel.IsFormVisible = false;
    }

}