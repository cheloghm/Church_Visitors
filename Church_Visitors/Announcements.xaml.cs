namespace Church_Visitors;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Announcements : ContentPage
{
	public Announcements()
	{
		InitializeComponent();
	}

    void OnSearchTextChanged(object sender, TextChangedEventArgs args)
    {
        // Your event handling code here
    }
}