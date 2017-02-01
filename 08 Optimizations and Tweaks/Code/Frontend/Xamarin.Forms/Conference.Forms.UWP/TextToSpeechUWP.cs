using System;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;
using Conference.Frontend;
using Conference.Forms.UWP;

[assembly: Xamarin.Forms.Dependency(typeof(TextToSpeechUWP))]
namespace Conference.Forms.UWP
{
    public class TextToSpeechUWP : ITextToSpeech
    {
        public async void Speak(string text)
        {
            MediaElement mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(text);
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }
    }
}
