using System.Security.Cryptography;
using System.Text;

namespace TheTowerAPI.Services
{
    public class SHA256Hasher
    {
        public SHA256Hasher() { }

        public string Hash(string toHash)
        {
            using SHA256 sha256Hash = SHA256.Create();
            
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(toHash));
            StringBuilder builder = new StringBuilder();
            foreach (var temp in bytes)
                builder.Append(temp.ToString("x2"));

            return builder.ToString();
        }
    }
}