using System.Text;
using System.Security.Cryptography;

namespace CoordinateRegistration.Application.Utils
{
    public class Cryptography
    {
        public static string GetMd5(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

            var result = string.Empty;

            foreach (var item in hash)
                result += item.ToString("x2");

            return result;
        }
    }
}
