using Conference.Frontend;
using ModernHttpClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace Conference.Forms
{
    public class FormsHttpService : IHttpService
    {
        private HttpClient httpClient;

        public FormsHttpService()
        {
            httpClient = new HttpClient(new NativeMessageHandler());
        }
        public async Task<string> GetStringAsync(string url)
		{
			return await httpClient.GetStringAsync(url);
        }
    }
}
