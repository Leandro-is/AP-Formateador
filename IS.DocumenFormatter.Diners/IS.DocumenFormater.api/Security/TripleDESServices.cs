using IS.DocumenFormater.api.Security.Exchange;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IS.DocumenFormater.api.Security
{
    public class TripleDESServices : ITripleDESServices
    {
        private readonly ITripleDESConfiguration _tripleDESConfiguration;
        public TripleDESServices(ITripleDESConfiguration tripleDESConfiguration)
        {
            _tripleDESConfiguration = tripleDESConfiguration;
        }
        public String Encrypt(string toEncrypt, bool useHashing = true)
        {
            byte[] keyArray, ivArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            string key = _tripleDESConfiguration.sEncryptionKey;
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                ivArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_tripleDESConfiguration.IV));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
                ivArray = UTF8Encoding.UTF8.GetBytes(_tripleDESConfiguration.IV);
            }
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.KeySize = 192;
            tdes.Mode = CipherMode.CBC;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateEncryptor(keyArray, ivArray);
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public String Decrypt(string cipherString, bool useHashing = true)
        {
            byte[] keyArray, ivArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);
            string key = _tripleDESConfiguration.sEncryptionKey;
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                ivArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_tripleDESConfiguration.IV));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
                ivArray = UTF8Encoding.UTF8.GetBytes(_tripleDESConfiguration.IV);
            }
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.KeySize = 192;
            tdes.Mode = CipherMode.CBC;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateDecryptor(keyArray, ivArray);
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        public async Task<String> EncryptAsync(string toEncrypt, bool useHashing = true)
        {
            return await Task.Run<String>(delegate ()
            {
                return Encrypt(toEncrypt, useHashing);
            });
        }
        public async Task<String> DecryptAsync(string cipherString, bool useHashing = true)
        {
            return await Task.Run<String>(delegate ()
            {
                return Decrypt(cipherString, useHashing);
            });
        }
    }
}
