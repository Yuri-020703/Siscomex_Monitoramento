using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SiscomexMonitor
{
    class ApiMonitor
    {
        public string Name { get; }
        public string Url { get; }

        public ApiMonitor(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}