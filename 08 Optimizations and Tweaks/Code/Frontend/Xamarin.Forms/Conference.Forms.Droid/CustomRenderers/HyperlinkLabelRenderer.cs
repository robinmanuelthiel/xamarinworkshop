using Conference.Forms.CustomControls;
using Conference.Forms.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(HyperlinkLabel), typeof(HyperlinkLabelRenderer))]
namespace Conference.Forms.Droid.CustomRenderers
{
	public class HyperlinkLabelRenderer : LabelRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				// Set TextView underlining
				Control.PaintFlags |= Android.Graphics.PaintFlags.UnderlineText;
			}
		}
	}
}