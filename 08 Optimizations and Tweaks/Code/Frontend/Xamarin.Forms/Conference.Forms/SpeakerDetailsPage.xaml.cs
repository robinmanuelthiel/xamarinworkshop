using System;
using System.Collections.Generic;
using Conference.Core;
using Conference.Frontend;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;

namespace Conference.Forms
{
	public partial class SpeakerDetailsPage : ContentPage
	{
		private SpeakerDetailsViewModel viewModel;

		public SpeakerDetailsPage(Speaker speaker)
		{			
			InitializeComponent();
			viewModel = SimpleIoc.Default.GetInstance<SpeakerDetailsViewModel>();
			viewModel.CurrentSpeaker = speaker;
			BindingContext = viewModel;
		}
	}
}
