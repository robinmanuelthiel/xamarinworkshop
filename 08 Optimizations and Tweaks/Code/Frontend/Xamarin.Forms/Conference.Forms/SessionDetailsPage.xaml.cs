using System;
using System.Collections.Generic;
using Conference.Core;
using Conference.Frontend;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;

namespace Conference.Forms
{
	public partial class SessionDetailsPage : ContentPage
	{
		private SessionDetailsViewModel viewModel;

		public SessionDetailsPage(Session session)
		{
			InitializeComponent();

			viewModel = SimpleIoc.Default.GetInstance<SessionDetailsViewModel>();
			viewModel.CurrentSession = session;
			BindingContext = viewModel;
		}

        protected override void OnAppearing()
        {
			var textToSpeechImpl = DependencyService.Get<ITextToSpeech>();
            if (textToSpeechImpl != null)
            {
                textToSpeechImpl.Speak(viewModel.CurrentSession.Name);
            }
        }
    }
}
