using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Frontend
{
    public interface IHttpService
    {
        Task<string> GetStringAsync(string url);
    }
}
