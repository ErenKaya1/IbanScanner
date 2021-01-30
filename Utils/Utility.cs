using System;
using System.Net.Mail;
using Enum;

namespace Utils
{
    public static class Utility
    {
        public static LoginProvider GetLoginProvider(string username)
        {
            try
            {
                var mailAdress = new MailAddress(username);
                return LoginProvider.Email;
            }
            catch (Exception)
            {
                return LoginProvider.Username;
            }
        }
    }
}