using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Helpers
{
    public static class Formatter
    {
        public static string FormatName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name) + " is not a valid input for a name");

            // Uppercase first letter, dean to Dean.
            name = name.First().ToString().ToUpper() + name.Substring(1);

            return name;
        }

        public static string FormatEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException(nameof(email));

            email = email.ToLower();

            var emailAddress = new System.Net.Mail.MailAddress(email);
            if(emailAddress.Address != email)
            {
                throw new ArgumentException(nameof(email) + " is not a valid input for an email");
            }

            return email;
        }
    }
}
