using Conference.Forms.CustomControls;
using Conference.Forms.UWP.CustomRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(HyperlinkLabel), typeof(HyperlinkLabelRenderer))]
namespace Conference.Forms.UWP.CustomRenderers
{
    class HyperlinkLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                // Set TextView underlining
                var underlinedText = new Underline();
                underlinedText.Inlines.Add(new Run { Text = Control.Text });
                Control.Text = string.Empty;
                Control.Inlines.Add(underlinedText);
            }
        }
    }
}
