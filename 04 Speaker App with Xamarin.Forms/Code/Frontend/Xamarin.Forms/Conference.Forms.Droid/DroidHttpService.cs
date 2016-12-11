using Conference.Frontend;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Android.OS;

[assembly: Xamarin.Forms.Dependency(typeof(Conference.Forms.Droid.DroidHttpService))]
namespace Conference.Forms.Droid
{
	public class DroidHttpService : IHttpService
	{
		private HttpClient httpClient;

		public DroidHttpService()
		{
			// HTTPS Fix 
			// see https://developer.xamarin.com/guides/cross-platform/transport-layer-security/
			httpClient = new HttpClient(new Xamarin.Android.Net.AndroidClientHandler());
		}

		public async Task<string> GetStringAsync(string url)
		{
			// TEMP fix for NetWorkOnMainThread
			// see http://stackoverflow.com/questions/6343166/how-to-fix-android-os-networkonmainthreadexception
			// and http://www.androiddesignpatterns.com/2012/06/app-force-close-honeycomb-ics.html
			StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().PermitAll().Build();
			StrictMode.SetThreadPolicy(policy);
			return await httpClient.GetStringAsync(url);
		}
	}
}
