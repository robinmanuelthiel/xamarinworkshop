using System;
using System.Collections.Generic;
using Conference.Core;
using Conference.Frontend;
using Xamarin.Forms;

namespace Conference.Forms
{
	public partial class SessionDetailsPage : ContentPage
	{
		private SessionDetailsViewModel viewModel;

		public SessionDetailsPage(Session session)
		{
			InitializeComponent();
			viewModel = new SessionDetailsViewModel();
			viewModel.CurrentSession = session;
			BindingContext = viewModel;
		}
	}
}
