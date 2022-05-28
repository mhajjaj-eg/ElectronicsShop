using System.Security.Cryptography;
using System.Text;

namespace ElectronicsShop.DAL.Helpers
{
    public static class StringHelper
    {
        public static bool IsNullOrWhiteSpace(this string str, params string[] keywords)
        {
            bool result = string.IsNullOrWhiteSpace(str);
            foreach (string keyword in keywords)
            {
                if (string.IsNullOrWhiteSpace(keyword)) result = true;
            }
            return result;
        }

        private static string EncryptionKey => "sad24345rgfd234mfgDrt66wsswe466fE35";

        public static string EncryptPassword(this string password)
        {
            byte[] txt = UTF8Encoding.UTF8.GetBytes(password);

            using (MD5CryptoServiceProvider md5 = new())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(EncryptionKey));

                using (TripleDESCryptoServiceProvider tripdes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripdes.CreateEncryptor();
                    byte[] result = transform.TransformFinalBlock(txt, 0, txt.Length);
                    return Convert.ToBase64String(result, 0, result.Length);
                }
            }
        }

        public static string DecryptPassword(this string encryptedPassword)
        {
            byte[] txt = Convert.FromBase64String(encryptedPassword);

            MD5CryptoServiceProvider md5 = new();
            byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(EncryptionKey));

            using (TripleDESCryptoServiceProvider tripdes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                ICryptoTransform transform = tripdes.CreateDecryptor();
                byte[] result = transform.TransformFinalBlock(txt, 0, txt.Length);
                return UTF8Encoding.UTF8.GetString(result);
            }
        }
    }
}
