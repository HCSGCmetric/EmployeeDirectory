using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeDirectory.Helper
{
    public class EncryptDecryptUtility
    {
        /// <summary>
        /// The initialize vector bytes
        /// </summary>
        private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");

        /// <summary>
        /// The key size.
        /// This constant is used to determine the key size of the encryption algorithm.
        /// </summary>
        private const int keysize = 256;

        /// <summary>
        /// The pass phrase
        /// </summary>
        public static string passPhrase = "asdasd";

        /// <summary>
        /// Encrypts the specified plain text.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <param name="test">if set to <c>true</c> [test].</param>
        /// <returns>Encrypted string.</returns>
        /// <updatedby>Rujuta Xavier</updatedby>
        /// <updatedon>11/03/2016</updatedon>
        /// <updates>Commented code removed.</updates>
        public static string Encrypt(string plainText, bool test)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(plainText);

                System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();

                // Get the key from config file
                string key = "";

                // If hashing use get hashcode regards to your key
                if (true)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                    // Always release the resources and flush data
                    // of the Cryptographic service provide. Best Practice
                    hashmd5.Clear();
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                // set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;

                // mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
                tdes.Mode = CipherMode.ECB;

                // padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();

                // transform the specified region of bytes array to resultArray
                byte[] resultArray = cTransform.TransformFinalBlock
                        (toEncryptArray, 0, toEncryptArray.Length);

                // Release resources held by TripleDes Encryptor
                tdes.Clear();

                // Return the encrypted data into unreadable string format
                return Convert.ToBase64String(resultArray, 0, resultArray.Length).Replace('+', '_').Replace('/', '-').Replace('=', '$');
            }
            catch
            {
                return plainText;
            }
        }

        /// <summary>
        /// Encrypts the specified plain text.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>Encrypted string.</returns>
        /// <updatedby>Rujuta Xavier</updatedby>
        /// <updatedon>11/03/2016</updatedon>
        /// <updates>Commented code removed.</updates>
        public static string Encrypt(string plainText)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(plainText);

                System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();

                // Get the key from config file
                string key = "";

                // System.Windows.Forms.MessageBox.Show(key);
                // If hashing use get hashcode regards to your key
                if (true)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                    // Always release the resources and flush data
                    // of the Cryptographic service provide. Best Practice
                    hashmd5.Clear();
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                // set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;

                //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
                tdes.Mode = CipherMode.ECB;

                //p adding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();

                // transform the specified region of bytes array to resultArray
                byte[] resultArray = cTransform.TransformFinalBlock
                        (toEncryptArray, 0, toEncryptArray.Length);

                // Release resources held by TripleDes Encryptor
                tdes.Clear();

                // Return the encrypted data into unreadable string format
                return Convert.ToBase64String(resultArray, 0, resultArray.Length).Replace('+', '_').Replace('/', '-').Replace('=', '$');
            }
            catch
            {
                return plainText;
            }
        }

        /// <summary>
        /// Decrypts the specified cipher text.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="test">if set to <c>true</c> [test].</param>
        /// <returns>Decrypted string.</returns>
        /// <updatedby>Rujuta Xavier</updatedby>
        /// <updatedon>11/03/2016</updatedon>
        /// <updates>Commented code removed.</updates>
        public static string Decrypt(string cipherText, bool test)
        {
            try
            {
                byte[] keyArray;

                // get the byte code of the string
                cipherText = cipherText.Replace('_', '+').Replace('-', '/').Replace('$', '=');
                byte[] toEncryptArray = Convert.FromBase64String(cipherText);
                System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();

                // Get your key from config file to open the lock!
                string key = "";

                if (true)
                {
                    // if hashing was used get the hash code with regards to your key
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                    // release any resource held by the MD5CryptoServiceProvider
                    hashmd5.Clear();
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                // set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;

                // mode of operation. there are other 4 modes.
                // We choose ECB(Electronic code Book)
                tdes.Mode = CipherMode.ECB;

                // padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock
                        (toEncryptArray, 0, toEncryptArray.Length);

                // Release resources held by TripleDes Encryptor
                tdes.Clear();

                // return the Clear decrypted TEXT
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return cipherText;
            }
        }

        /// <summary>
        /// Decrypts the specified cipher text.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <returns>Decrypted string.</returns>
        /// <updatedby>Rujuta Xavier</updatedby>
        /// <updatedon>11/03/2016</updatedon>
        /// <updates>Commented code removed.</updates>
        public static string Decrypt(string cipherText)
        {
            try
            {
                byte[] keyArray;

                // get the byte code of the string
                cipherText = cipherText.Replace('_', '+').Replace('-', '/').Replace('$', '=');
                byte[] toEncryptArray = Convert.FromBase64String(cipherText);
                System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();

                // Get your key from config file to open the lock!
                string key = string.Empty;

                if (true)
                {
                    // if hashing was used get the hash code with regards to your key
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                    // release any resource held by the MD5CryptoServiceProvider
                    hashmd5.Clear();
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                // set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;

                // mode of operation. there are other 4 modes.
                // We choose ECB(Electronic code Book)
                tdes.Mode = CipherMode.ECB;

                // padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock
                        (toEncryptArray, 0, toEncryptArray.Length);

                // Release resources held by TripleDes Encryptor
                tdes.Clear();

                // return the Clear decrypted TEXT
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return cipherText;
            }
        }

        /// <summary>
        /// Redirects the action parameter.
        /// </summary>
        /// <param name="inputQuerystring">The input query string.</param>
        /// <returns>Redirects to given action.</returns>
        /// <updatedby>Rujuta Xavier</updatedby>
        /// <updatedon>11/03/2016</updatedon>
        /// <updates>Commented code removed.</updates>
        public static string RedirecttoActionParameter(string inputQuerystring)
        {
            return Encrypt(inputQuerystring);
        }
    }
}