using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace World.Services
{
    public interface IMailService
    {
        void SendMessage(string from, string to, string subject, string message);
    }
}
