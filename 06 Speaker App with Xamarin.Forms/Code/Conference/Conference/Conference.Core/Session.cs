using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Core
{
    public class Session
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public DateTime StartTime { get; set; }
        public string SpeakerId { get; set; }
    }
}
