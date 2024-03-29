﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class LocalMailService
    {
        private string _mailTo = "admin@test.com";
        private string _mailFrom = "test@test.com";

        public void Send (string subject, string message)
        {
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, testing the LocalMailService");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }
}
