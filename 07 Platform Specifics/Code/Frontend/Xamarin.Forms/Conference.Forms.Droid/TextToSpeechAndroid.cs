using Android.Speech.Tts;
using Conference.Forms.Droid;
using Conference.Frontend;

[assembly: Xamarin.Forms.Dependency(typeof(TextToSpeechAndroid))]
namespace Conference.Forms.Droid
{
	public class TextToSpeechAndroid : Java.Lang.Object, ITextToSpeech, TextToSpeech.IOnInitListener
	{
		TextToSpeech speaker;
		string toSpeak;

		public void Speak(string text)
		{
			toSpeak = text;
			speaker = new TextToSpeech(Xamarin.Forms.Forms.Context, this);
		}

		public void OnInit(OperationResult status)
		{
			if (status.Equals(OperationResult.Success))
				speaker.Speak(toSpeak, QueueMode.Flush, null, null);
		}
	}
}