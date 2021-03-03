using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Settings
{
    public class EmailSettings
    {
        public string Host { get; set; }
        public int SmptPort { get; set; }
        public bool UseSSL { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }      
    }
}
