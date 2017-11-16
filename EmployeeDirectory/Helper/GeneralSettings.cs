using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Cryptography;
namespace EmployeeDirectory.Helper
{
    public static class GeneralSettings
    {
        public static string ConnectionString { get; set; }
        public static string GetConnectionString(string ConnectionStringName)
        {
            return EncryptDecryptUtility.Decrypt(Convert.ToString(ConfigurationManager.ConnectionStrings[ConnectionStringName]));
            //return Convert.ToString(ConfigurationManager.ConnectionStrings[ConnectionStringName]);
        }
        public static string Decrypt(string cipherText)
        {
            try
            {
                //int numberChars = cipherText.Length;
                //byte[] bytes = new byte[numberChars / 2];
                //for (int i = 0; i < numberChars; i += 2)
                //{
                //    bytes[i / 2] = Convert.ToByte(cipherText.Substring(i, 2), 16);
                //}
                //return System.Text.Encoding.Unicode.GetString(bytes);
                byte[] keyArray;
                //get the byte code of the string
                //cipherText = cipherText.Replace('_', '+').Replace('-', '/').Replace('$', '=');


                byte[] toEncryptArray = Convert.FromBase64String(cipherText);

                System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
                //Get your key from config file to open the lock!
                string key = "";

                if (true)
                {
                    //if hashing was used get the hash code with regards to your key
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    //release any resource held by the MD5CryptoServiceProvider

                    hashmd5.Clear();
                }
                else
                {
                    //if hashing was not implemented get the byte code of the key
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                //set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;
                //mode of operation. there are other 4 modes.
                //We choose ECB(Electronic code Book)

                tdes.Mode = CipherMode.ECB;
                //padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock
                        (toEncryptArray, 0, toEncryptArray.Length);
                //Release resources held by TripleDes Encryptor
                tdes.Clear();
                //return the Clear decrypted TEXT
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return cipherText;
            }
        }
    }
}