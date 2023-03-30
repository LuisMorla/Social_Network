using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application.Helpers
{
    public class PasswordEncryption
    {
        public static string ComputeSha256Hash(string password)
        {
            //Create a SHA256
            using (SHA256 SHA256Hash = SHA256.Create())
            {
                byte[] bytes = SHA256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                //Convert byte array to a string
                StringBuilder builder = new();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }

        }
    }
}
