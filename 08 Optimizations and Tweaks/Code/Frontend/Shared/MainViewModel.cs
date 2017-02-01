using Conference.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Frontend
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IConferenceService conferenceService;

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Speaker> speakers;
        public ObservableCollection<Speaker> Speakers
        {
            get { return speakers; }
            set { speakers = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Session> sessions;
        public ObservableCollection<Session> Sessions
        {
            get { return sessions; }
            set { sessions = value; OnPropertyChanged(); }
        }

        public MainViewModel(IConferenceService conferenceServiceImpl)
        {
            conferenceService = conferenceServiceImpl;
        }

        public async Task RefreshAsync()
        {
            IsBusy = true;

            var sessions = await conferenceService.GetSessionsAsync();
            var speakers = await conferenceService.GetSpeakersAsync();
            Sessions = new ObservableCollection<Session>(sessions);
            Speakers = new ObservableCollection<Speaker>(speakers);

            IsBusy = false;
        }

        // Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
