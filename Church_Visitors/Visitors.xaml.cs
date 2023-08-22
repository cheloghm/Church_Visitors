namespace Church_Visitors;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Visitors : ContentPage
{
	public Visitors()
	{
		InitializeComponent();
	}

    void OnSearchTextChanged(object sender, TextChangedEventArgs args)
    {
        // Your event handling code here
    }
}