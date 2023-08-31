using System.Threading.Tasks;
using Church_Visitors.DTO;
using Church_Visitors.Interfaces;
using Church_Visitors.ViewModels;
using Microsoft.Maui.Controls;

namespace Church_Visitors.Services
{
    public class AlertService : IAlertService
    {
        public Task ShowAlert(string title, string message)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }

        public async Task ShowVisitorDetailsAsync(VisitorDTO visitor)
        {
            // Create a formatted string to display visitor details
            string details = $"Full Name: {visitor.FullName}\n" +
                             $"Guest Of: {visitor.GuestOf}\n" +
                             $"Date Visited: {visitor.DateEntered}\n" +
                             $"Other Remarks: {visitor.OtherRemarks}";

            await Application.Current.MainPage.DisplayAlert("Visitor Details", details, "OK");
        }

        public async Task ShowUpdateVisitorAsync(VisitorDTO visitor, VisitorsViewModel viewModel)
        {
            // Create a custom content view for the update form with pre-populated data
            var updateForm = new StackLayout();

            var fullNameEntry = new Entry { Placeholder = "Full Name", Text = visitor.FullName };
            var guestOfEntry = new Entry { Placeholder = "Guest Of", Text = visitor.GuestOf };
            var otherRemarksEntry = new Entry { Placeholder = "Other Remarks", Text = visitor.OtherRemarks };

            var submitButton = new Button { Text = "Submit" };
            submitButton.Clicked += async (sender, args) =>
            {
                // Get updated values from the entries and perform update logic here
                visitor.FullName = fullNameEntry.Text;
                visitor.GuestOf = guestOfEntry.Text;
                visitor.OtherRemarks = otherRemarksEntry.Text;

                viewModel.UpdateVisitor(visitor); // Call the method to update properties

                // Close the modal after the update
                if (Application.Current.MainPage.Navigation.ModalStack.Count > 0)
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
            };

            updateForm.Children.Add(fullNameEntry);
            updateForm.Children.Add(guestOfEntry);
            updateForm.Children.Add(otherRemarksEntry);
            updateForm.Children.Add(submitButton);

            var modalPage = new ContentPage { Content = updateForm };
            await Application.Current.MainPage.Navigation.PushModalAsync(modalPage);
        }

        public async Task<bool> ShowConfirmationAlertAsync(string title, string message)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, "Yes", "No");
        }
    }
}
