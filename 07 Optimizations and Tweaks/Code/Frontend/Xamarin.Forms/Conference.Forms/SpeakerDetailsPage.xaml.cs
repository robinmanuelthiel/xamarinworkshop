using System;
using System.Collections.Generic;
using Conference.Core;
using Conference.Frontend;
using Xamarin.Forms;
using GalaSoft.MvvmLight.Ioc;

namespace Conference.Forms
{
	public partial class SpeakerDetailsPage : ContentPage
	{
		private SpeakerDetailsViewModel viewModel;

		public SpeakerDetailsPage(Speaker speaker)
		{
			InitializeComponent();

            // Get ViewModel from IoC Container
            viewModel = SimpleIoc.Default.GetInstance<SpeakerDetailsViewModel>();
            viewModel.CurrentSpeaker = speaker;
			BindingContext = viewModel;
		}
	}
}
