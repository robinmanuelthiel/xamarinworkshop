using AVFoundation;
using Conference.Forms.iOS;
using Conference.Frontend;

[assembly: Xamarin.Forms.Dependency(typeof(TextToSpeechiOS))]
namespace Conference.Forms.iOS
{
	public class TextToSpeechiOS : ITextToSpeech
	{
		public void Speak(string text)
		{
			var speechSynthesizer = new AVSpeechSynthesizer();
			var speechUtterance = new AVSpeechUtterance(text);
			speechSynthesizer.SpeakUtterance(speechUtterance);
		}
	}
}