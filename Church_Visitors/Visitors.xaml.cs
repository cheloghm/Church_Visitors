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
        var viewModel = new VisitorsViewModel(
    ((App)Application.Current).ServiceProvider.GetService<IVisitorService>(),
    ((App)Application.Current).ServiceProvider.GetService<IAlertService>()
);
        BindingContext = viewModel;
    }

    void OnSearchTextChanged(object sender, TextChangedEventArgs args)
    {
        // Your event handling code here
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
}