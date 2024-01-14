using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Helpers
{
    public class SHA1Helper
    {
        public static string ComputeSHA1Hash(string input)
        {
            using (var sha1 = SHA1.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = sha1.ComputeHash(inputBytes);

                var stringBuilder = new StringBuilder();
                foreach (var item in hashBytes)
                {
                    stringBuilder.Append(item.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
