using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Conference.Core;

namespace Conference.Frontend
{
	public interface IConferenceService
	{
		Task<List<Session>> GetSessionsAsync();
		Task<List<Speaker>> GetSpeakersAsync();
	}
}