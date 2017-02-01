using System;
using Xamarin.Forms;

namespace Conference.Forms.CustomControls
{
    public class HyperlinkLabel : Label
    {
        public static readonly BindableProperty UriProperty = BindableProperty.Create(nameof(Uri), typeof(string), typeof(HyperlinkLabel), null);
        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        public HyperlinkLabel()
        {
            // Set text color
            TextColor = Color.Accent;

            // Underlining is set by custom renderers
            // On Android and UWP only, as it is against the iOS design guidelines

            // Add interaction
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += delegate { if (Uri != null) { Device.OpenUri(new Uri(Uri)); }};
            GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}
