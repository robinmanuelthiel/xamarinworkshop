
using Xamarin.Forms;

namespace Conference.Forms
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			var content = new MainPage();
			MainPage = new NavigationPage(content);

		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
