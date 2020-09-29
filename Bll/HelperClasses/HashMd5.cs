using Bll.Abstract;
using System.Security.Cryptography;
using System.Text;

namespace Bll.HelperClasses
{
    public class HashMd5 : IHashing
    {
        public string GetHashString(string str)
        {
            string hash;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, str);
            }
            return hash;
        }
        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
