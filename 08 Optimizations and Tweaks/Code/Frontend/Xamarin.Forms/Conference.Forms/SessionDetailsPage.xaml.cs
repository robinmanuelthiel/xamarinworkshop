using System;
using System.Collections.Generic;
using Conference.Core;
using Conference.Frontend;
using Xamarin.Forms;
using GalaSoft.MvvmLight.Ioc;

namespace Conference.Forms
{
	public partial class SessionDetailsPage : ContentPage
	{
		private SessionDetailsViewModel viewModel;

		public SessionDetailsPage(Session session)
		{
			InitializeComponent();

            // Get ViewModel from IoC Container
            viewModel = SimpleIoc.Default.GetInstance<SessionDetailsViewModel>();
            viewModel.CurrentSession = session;
			BindingContext = viewModel;
		}
	}
}
