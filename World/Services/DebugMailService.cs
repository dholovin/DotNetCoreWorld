using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace World.Services
{
    public class DebugMailService : IMailService
    {
        public void SendMessage(string from, string to, string subject, string message)
        {
            Debug.WriteLine($"Message Sent from: {from} to: {to}  subject: {subject} message: {message}");
        }
    }
}
