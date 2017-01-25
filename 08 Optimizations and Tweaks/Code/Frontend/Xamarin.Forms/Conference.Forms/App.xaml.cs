
using Conference.Frontend;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace Conference.Forms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Create Inversion of Control container
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Register Services
            SimpleIoc.Default.Register<IHttpService, FormsHttpService>();
            SimpleIoc.Default.Register<IConferenceService, HttpConferenceService>();

            // Register ViewModels
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SessionDetailsViewModel>();
            SimpleIoc.Default.Register<SpeakerDetailsViewModel>();

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
