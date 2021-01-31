using System;
using System.IO;
using System.Net.Mail;
using Enum;
using Microsoft.AspNetCore.Http;

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

        public static bool IsImage(this IFormFile file)
        {
            switch (Path.GetExtension(file.FileName).ToLower())
            {
                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".jfif":
                case ".gif":
                case ".tiff":
                case ".pjp":
                case ".svg":
                case ".bmp":
                case ".webp":
                case ".ico":
                case ".tif":
                case ".avif":
                    return true;
                default:
                    return false;
            }
        }
    }
}