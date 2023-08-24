using Church_Visitors.ViewModels;

namespace Church_Visitors;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Visitors : ContentPage
{
    public Visitors()
    {
        InitializeComponent();
        BindingContext = ((App)Application.Current).ServiceProvider.GetService<VisitorsViewModel>();
    }

    void OnSearchTextChanged(object sender, TextChangedEventArgs args)
    {
        // Your event handling code here
    }
}