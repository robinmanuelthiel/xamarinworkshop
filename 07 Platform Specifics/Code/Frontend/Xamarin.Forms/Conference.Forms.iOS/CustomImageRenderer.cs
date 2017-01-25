using System;
using Conference.Forms.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Image), typeof(CustomImageRenderer))]
namespace Conference.Forms.iOS
{
	public class CustomImageRenderer : ImageRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Image> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.Layer.CornerRadius = Control.Image.Size.Width / 2;
				Control.ClipsToBounds = true;
			}
		}
	}
}
