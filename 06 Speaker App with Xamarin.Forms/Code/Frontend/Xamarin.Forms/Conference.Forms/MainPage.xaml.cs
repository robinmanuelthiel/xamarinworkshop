using Conference.Frontend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Conference.Forms
{
    public partial class MainPage : TabbedPage
    {
        private MainViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();

            var httpService = new FormsHttpService();
            var conferenceService = new ConferenceService(httpService);
            viewModel = new MainViewModel(conferenceService);

            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.RefreshAsync();
        }
    }
}
