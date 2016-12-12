using Conference.Frontend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Conference.Core;
using GalaSoft.MvvmLight.Ioc;

namespace Conference.Forms
{
    public partial class MainPage : TabbedPage
    {
        private MainViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();

            // Get ViewModel from IoC Container
            viewModel = SimpleIoc.Default.GetInstance<MainViewModel>();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.RefreshAsync();
        }

		private void Session_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			// Get selected session
			var selectedSession = e.SelectedItem as Session;
			if (selectedSession != null)
			{
				// Navigate to details page and provide selected session
				Navigation.PushAsync(new SessionDetailsPage(selectedSession));
			}

			// Unselect item
			(sender as ListView).SelectedItem = null;
		}

		private void Speaker_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			// Get selected session
			var selectedSpeaker = e.SelectedItem as Speaker;
			if (selectedSpeaker != null)
			{
				// Navigate to details page and provide selected session
				Navigation.PushAsync(new SpeakerDetailsPage(selectedSpeaker));
			}

			// Unselect item
			(sender as ListView).SelectedItem = null;
		}
    }
}
