using Conference.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Conference.Frontend
{
    public class ConferenceService
    {
        private HttpClient httpClient;

        public ConferenceService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Session>> GetSessionsAsync()
        {
            var json = await httpClient.GetStringAsync("");
            var sessions = JsonConvert.DeserializeObject<List<Session>>(json);
            return sessions;
        }

        public async Task<List<Speaker>> GetSpeakersAsync()
        {
            var speakers = new List<Speaker>();
            speakers.Add(new Speaker { Id = "1", Name = "Kurt Mendoza", Title = "Technical Evangelist", Bio = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.", ImagePath = "https://randomuser.me/api/portraits/men/9.jpg" });
            speakers.Add(new Speaker { Id = "2", Name = "Pauline Price", Title = "Account Executive", Bio = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.", ImagePath = "https://randomuser.me/api/portraits/women/71.jpg" });
            speakers.Add(new Speaker { Id = "3", Name = "Liam Wagner", Title = "Technical Solutions Professional", Bio = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.", ImagePath = "https://randomuser.me/api/portraits/men/83.jpg" });

            var json = JsonConvert.SerializeObject(speakers);


            return speakers;
        }
    }
}