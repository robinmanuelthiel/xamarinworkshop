using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Conference.Core;
using Conference.Frontend;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace Conference.Forms
{
	public class AzureConferenceService : IConferenceService
	{
		private MobileServiceClient client;
		private IMobileServiceSyncTable<Session> sessionTable;
		private IMobileServiceSyncTable<Speaker> speakerTable;

		public AzureConferenceService()
		{
			client = new MobileServiceClient("https://xamarinworkshopapp.azurewebsites.net");
		}

		public async Task InitAsync()
		{
			// Setup local database
			var path = Path.Combine(MobileServiceClient.DefaultDatabasePath, "syncstore.db");
			var store = new MobileServiceSQLiteStore(path);

			// Define local tables to sync with
			store.DefineTable<Session>();
			store.DefineTable<Speaker>();

			// Initialize SyncContext
			await client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

			// Get our sync table that will call out to azure
			sessionTable = client.GetSyncTable<Session>();
			speakerTable = client.GetSyncTable<Speaker>();
		}

		public async Task<List<Session>> GetSessionsAsync()
		{
			await InitAsync();
			await SyncAsync();
			return await sessionTable.ToListAsync();
		}

		public async Task<List<Speaker>> GetSpeakersAsync()
		{
			await InitAsync();
			await SyncAsync();
			return await speakerTable.ToListAsync();
		}

		public async Task SyncAsync()
		{
			try
			{
				await client.SyncContext.PushAsync();
				await sessionTable.PullAsync("allSessions", sessionTable.CreateQuery());
				await speakerTable.PullAsync("allSpeakers", speakerTable.CreateQuery());
			}
			catch (Exception ex)
			{
				// Unable to sync speakers, that is alright as we have offline capabilities: " + ex);
			}
		}
	}
}