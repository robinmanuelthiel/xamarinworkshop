using Conference.Frontend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Conference.Core;

namespace Conference.Forms
{
	public partial class MainPage : TabbedPage
	{
		private MainViewModel viewModel;

		public MainPage()
		{
			InitializeComponent();

			var httpService = new FormsHttpService();
			//var conferenceService = new HttpConferenceService(httpService);
			var conferenceService = new AzureConferenceService();
			viewModel = new MainViewModel(conferenceService);

			BindingContext = viewModel;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await viewModel.RefreshAsync();
		}

		private async void Session_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			// Get selected session
			var selectedSession = e.SelectedItem as Session;
			if (selectedSession != null)
			{
				// Navigate to details page and provide selected session
				await Navigation.PushAsync(new SessionDetailsPage(selectedSession));
			}

			// Unselect item
			(sender as ListView).SelectedItem = null;
		}

		private async void Speaker_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			// Get selected session
			var selectedSpeaker = e.SelectedItem as Speaker;
			if (selectedSpeaker != null)
			{
				// Navigate to details page and provide selected session
				await Navigation.PushAsync(new SpeakerDetailsPage(selectedSpeaker));
			}

			// Unselect item
			(sender as ListView).SelectedItem = null;
		}

		private async void AboutToolbarItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AboutPage());
		}
	}
}
