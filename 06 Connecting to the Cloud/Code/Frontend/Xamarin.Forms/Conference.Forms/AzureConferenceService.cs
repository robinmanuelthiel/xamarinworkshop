using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Conference.Core;
using Conference.Frontend;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace Conference.Forms
{
	public class AzureConferenceService : IConferenceService
	{
		private MobileServiceClient client;
		private IMobileServiceTable<Session> sessionTable;
		private IMobileServiceTable<Speaker> speakerTable;

		public AzureConferenceService()
		{
			client = new MobileServiceClient("https://xamarinworkshopapp.azurewebsites.net");
			sessionTable = client.GetTable<Session>();
			speakerTable = client.GetTable<Speaker>();
		}

		public async Task<List<Session>> GetSessionsAsync()
		{
			return await sessionTable.ToListAsync();
		}

		public async Task<List<Speaker>> GetSpeakersAsync()
		{
			return await speakerTable.ToListAsync();
		}
	}
}