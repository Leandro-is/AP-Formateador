using System;
using System.Security.Cryptography;
using System.Text;

namespace IS.DocumenFormater.api.Security
{
    public static class TripleDES
    {
        //Desarrollo
        private static String IV = "Ar8P[YRcr=v38~5*AcD@<^u^";
        private static String sEncryptionKey = "zUTL[59VnQepJ_G]Y}zc5ZR-(QXjTk,_";

        ////Producción
        //private static String IV = "F;;VX;$ucXw8k6rR$3+K8)M`";
        //private static String sEncryptionKey = "=5R'}%jYGRhrv2.BCDfy+fv+'.~(H>Np";

        public static string Encrypt(string toEncrypt, bool useHashing = true)
        {
            byte[] keyArray, ivArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            string key = sEncryptionKey;
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                ivArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(IV));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
                ivArray = UTF8Encoding.UTF8.GetBytes(IV);
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
        public static string Decrypt(string cipherString, bool useHashing = true)
        {
            byte[] keyArray, ivArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);
            string key = sEncryptionKey;
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                ivArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(IV));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
                ivArray = UTF8Encoding.UTF8.GetBytes(IV);
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

        //private static byte[] key = { };
        //private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
        //private static String sEncryptionKey = "G]8Mg$d%";

        //public static string Decrypt(string stringToDecrypt)
        //{
        //  if (String.IsNullOrEmpty(stringToDecrypt)) return stringToDecrypt;
        //  byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
        //  try
        //  {
        //    key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
        //    System.Text.Encoding.UTF8.GetString(IV);
        //    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //    inputByteArray = Convert.FromBase64String(stringToDecrypt);
        //    MemoryStream ms = new MemoryStream();
        //    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
        //    cs.Write(inputByteArray, 0, inputByteArray.Length);
        //    cs.FlushFinalBlock();
        //    System.Text.Encoding encoding = System.Text.Encoding.UTF8;
        //    return encoding.GetString(ms.ToArray());
        //  }
        //  catch (Exception e)
        //  {
        //    return e.Message;
        //  }
        //}

        //public static string Encrypt(object stringToEncrypt)
        //{
        //  return Encrypt(stringToEncrypt.ToString());
        //}

        //public static string Encrypt(string stringToEncrypt)
        //{
        //  if (String.IsNullOrEmpty(stringToEncrypt)) return stringToEncrypt;
        //  try
        //  {
        //    key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
        //    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //    byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
        //    MemoryStream ms = new MemoryStream();
        //    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
        //    cs.Write(inputByteArray, 0, inputByteArray.Length);
        //    cs.FlushFinalBlock();
        //    return Convert.ToBase64String(ms.ToArray());
        //  }
        //  catch (Exception e)
        //  {
        //    return e.Message;
        //  }
        //}

        ////private static void EncryptFile(String inName, String outName)
        ////{
        ////  FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
        ////  FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
        ////  fout.SetLength(0);
        ////  byte[] bin = new byte[100];
        ////  long rdlen = 0;
        ////  long totlen = fin.Length;
        ////  int len;
        ////  DES des = new DESCryptoServiceProvider();
        ////  CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
        ////  while (rdlen < totlen)
        ////  {
        ////    len = fin.Read(bin, 0, 100);
        ////    encStream.Write(bin, 0, len);
        ////    rdlen = rdlen + len;
        ////  }
        ////  encStream.Close();
        ////  fout.Close();
        ////  fin.Close();
        ////}
    }
}
