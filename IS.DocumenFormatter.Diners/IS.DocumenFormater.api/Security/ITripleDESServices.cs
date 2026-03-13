using System;
using System.Threading.Tasks;

namespace IS.DocumenFormater.api.Security
{
    public interface ITripleDESServices
    {
        String Encrypt(string toEncrypt, bool useHashing = true);
        String Decrypt(string cipherString, bool useHashing = true);
        Task<String> EncryptAsync(string toEncrypt, bool useHashing = true);
        Task<String> DecryptAsync(string cipherString, bool useHashing = true);
    }
}
