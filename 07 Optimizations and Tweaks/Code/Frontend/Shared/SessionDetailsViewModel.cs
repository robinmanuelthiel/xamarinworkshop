using Conference.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Frontend
{
    public class SessionDetailsViewModel : INotifyPropertyChanged
    {
        private Session currentSession;
        public Session CurrentSession
        {
            get { return currentSession; }
            set { currentSession = value; OnPropertyChanged(); }
        }

        // Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
