
namespace Phoneword;

public partial class MainPage : ContentPage
{
	int count = 0;
	string translatedNum;

    public MainPage()
	{
		InitializeComponent();
	}

	private void OnTranslate(object sender, EventArgs e) {
		string enteredNum = PhoneNumberText.Text;
		translatedNum = PhonewordTranslator.ToNumber(enteredNum);
		if (!string.IsNullOrEmpty(translatedNum)) {
			CallButton.IsEnabled = true;
			CallButton.Text = "Call" + translatedNum;
		} else {
			CallButton.IsEnabled = false;
			CallButton.Text = "Call";
		}
	}

	public async void OnCall(object sender, EventArgs e) {
		if(await this.DisplayAlert(
			"Dial a Number",
			"Would you like to call " + translatedNum + "?", "Yes", "No")) {
            try {
                PhoneDialer.Default.Open(translatedNum);
            } catch (ArgumentNullException) {
                await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
            } catch (FeatureNotSupportedException) {
                await DisplayAlert("Unable to dial", "Phone dialing not supported.", "OK");
            } catch (Exception) {
                // Other error has occurred.
                await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
            }
        }
	}
}

