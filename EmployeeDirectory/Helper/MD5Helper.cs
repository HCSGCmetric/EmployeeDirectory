using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EmployeeDirectory.Helper
{
    public class MD5Helper
    {
        /// <summary>
        /// The private key
        /// </summary>
        private const string PrivateKey = "<RSAKeyValue><Modulus>s6lpjspk+3o2GOK5TM7JySARhhxE5gB96e9XLSSRuWY2W9F951MfistKRzVtg0cjJTdSk5mnWAVHLfKOEqp8PszpJx9z4IaRCwQ937KJmn2/2VyjcUsCsor+fdbIHOiJpaxBlsuI9N++4MgF/jb0tOVudiUutDqqDut7rhrB/oc=</Modulus><Exponent>AQAB</Exponent><P>3J2+VWMVWcuLjjnLULe5TmSN7ts0n/TPJqe+bg9avuewu1rDsz+OBfP66/+rpYMs5+JolDceZSiOT+ACW2Neuw==</P><Q>0HogL5BnWjj9BlfpILQt8ajJnBHYrCiPaJ4npghdD5n/JYV8BNOiOP1T7u1xmvtr2U4mMObE17rZjNOTa1rQpQ==</Q><DP>jbXh2dVQlKJznUMwf0PUiy96IDC8R/cnzQu4/ddtEe2fj2lJBe3QG7DRwCA1sJZnFPhQ9svFAXOgnlwlB3D4Gw==</DP><DQ>evrP6b8BeNONTySkvUoMoDW1WH+elVAH6OsC8IqWexGY1YV8t0wwsfWegZ9IGOifojzbgpVfIPN0SgK1P+r+kQ==</DQ><InverseQ>LeEoFGI+IOY/J+9SjCPKAKduP280epOTeSKxs115gW1b9CP4glavkUcfQTzkTPe2t21kl1OrnvXEe5Wrzkk8rA==</InverseQ><D>HD0rn0sGtlROPnkcgQsbwmYs+vRki/ZV1DhPboQJ96cuMh5qeLqjAZDUev7V2MWMq6PXceW73OTvfDRcymhLoNvobE4Ekiwc87+TwzS3811mOmt5DJya9SliqU/ro+iEicjO4v3nC+HujdpDh9CVXfUAWebKnd7Vo5p6LwC9nIk=</D></RSAKeyValue>";

        /// <summary>
        /// The public key
        /// </summary>
        private const string PublicKey = "<RSAKeyValue><Modulus>s6lpjspk+3o2GOK5TM7JySARhhxE5gB96e9XLSSRuWY2W9F951MfistKRzVtg0cjJTdSk5mnWAVHLfKOEqp8PszpJx9z4IaRCwQ937KJmn2/2VyjcUsCsor+fdbIHOiJpaxBlsuI9N++4MgF/jb0tOVudiUutDqqDut7rhrB/oc=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        /// <summary>
        /// The encoder
        /// </summary>
        private static readonly UnicodeEncoding Encoder = new UnicodeEncoding();

        /// <summary>
        /// The value delimiter
        /// </summary>
        public const string ValueDelimiter = "[DELIM]";

        /// <summary>
        /// Converts value to encoding and calculates md5.
        /// </summary>
        /// <param name="value">Input string.</param>
        /// <returns>String object.</returns>
        public static string Utf8MD5Hex(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            using (var prov = new MD5CryptoServiceProvider())
            {
                byte[] bs = prov.ComputeHash(Encoding.UTF8.GetBytes(value));
                var s = new StringBuilder();
                foreach (byte b in bs)
                {
                    s.Append(b.ToString("x2", CultureInfo.InvariantCulture));
                }

                return s.ToString();
            }
        }

        /// <summary>
        /// The method create a Base64 encoded string from a normal string.
        /// </summary>
        /// <param name="toEncode">The String containing the characters to encode.</param>
        /// <returns>The Base64 encoded string.</returns>
        public static string EncodeTo64(string toEncode)
        {
            var toEncodeAsBytes = Encoding.Unicode.GetBytes(toEncode);

            var returnValue = Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;
        }

        /// <summary>
        /// The method to Decode your Base64 strings.
        /// </summary>
        /// <param name="encodedData">The String containing the characters to decode.</param>
        /// <returns>A String containing the results of decoding the specified sequence of bytes.</returns>
        public static string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes = Convert.FromBase64String(encodedData);

            string returnValue = Encoding.Unicode.GetString(encodedDataAsBytes);

            return returnValue;
        }

        /// <summary>
        /// Encrypts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>Returns object.</returns>
        public static string Encrypt(string data)
        {
            byte[] encryptedByteArray;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(PublicKey);
                var dataToEncrypt = Encoder.GetBytes(data);
                encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            }

            var length = encryptedByteArray.Count();
            var item = 0;
            var sb = new StringBuilder();
            foreach (var x in encryptedByteArray)
            {
                item++;
                sb.Append(x);

                if (item < length)
                {
                    sb.Append(",");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Decrypts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>Decrypt string data.</returns>
        public static string Decrypt(string data)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                if (data != null)
                {
                    var dataArray = data.Split(new[] { ',' });
                    var dataByte = new byte[dataArray.Length];
                    for (int i = 0; i < dataArray.Length; i++)
                    {
                        dataByte[i] = Convert.ToByte(dataArray[i], CultureInfo.InvariantCulture);
                    }

                    rsa.FromXmlString(PrivateKey);
                    var decryptedByte = rsa.Decrypt(dataByte, false);
                    return Encoder.GetString(decryptedByte);
                }
            }

            return null;
        }

        /// <summary>
        /// Objects to string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>String object.</returns>
        public static string ObjectToString(object value)
        {
            byte[] serializedArray;
            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, value);
                serializedArray = ms.ToArray();
            }

            return Convert.ToBase64String(serializedArray);
        }

        /// <summary>
        /// Strings to object.
        /// </summary>
        /// <param name="value">The base64 string.</param>
        /// <returns>Returns object</returns>
        public static object StringToObject(string value)
        {
            var bytes = Convert.FromBase64String(value);
            object formatter;
            using (var ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                formatter = new BinaryFormatter().Deserialize(ms);
            }

            return formatter;
        }

        #region EncryptData

        /// <summary>
        /// Encrypts the data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>String Object</returns>
        public static string EncryptData(string data)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();
            using (var hashProvider = new MD5CryptoServiceProvider())
            {
                var tdesKey = hashProvider.ComputeHash(utf8.GetBytes("x2"));
                using (var tdesAlgorithm = new TripleDESCryptoServiceProvider())
                {
                    tdesAlgorithm.Key = tdesKey;
                    tdesAlgorithm.Mode = CipherMode.ECB;
                    tdesAlgorithm.Padding = PaddingMode.PKCS7;
                    byte[] dataToEncrypt = utf8.GetBytes(data);
                    try
                    {
                        var encryptor = tdesAlgorithm.CreateEncryptor();
                        results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
                    }
                    finally
                    {
                        tdesAlgorithm.Clear();
                        hashProvider.Clear();
                    }
                }
            }

            return Convert.ToBase64String(results);
        }
        #endregion

        #region DecryptData
        /// <summary>
        /// Decrypts the data.
        /// </summary>
        /// <param name="data">The message.</param>
        /// <returns>Decrypted data</returns>
        public static string DecryptData(string data)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();
            using (var hashProvider = new MD5CryptoServiceProvider())
            {
                var tdesKey = hashProvider.ComputeHash(utf8.GetBytes("x2"));
                using (var tdesAlgorithm = new TripleDESCryptoServiceProvider())
                {
                    tdesAlgorithm.Key = tdesKey;
                    tdesAlgorithm.Mode = CipherMode.ECB;
                    tdesAlgorithm.Padding = PaddingMode.PKCS7;
                    var dataToDecrypt = Convert.FromBase64String(data);
                    try
                    {
                        ICryptoTransform decryptor = tdesAlgorithm.CreateDecryptor();
                        results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
                    }
                    finally
                    {
                        tdesAlgorithm.Clear();
                        hashProvider.Clear();
                    }
                }
            }

            return utf8.GetString(results);
        }
        #endregion

        /// <summary>
        /// Gets the serial number.
        /// </summary>
        /// <returns></returns>
        public static string GetSerialNumber()
        {
            Guid serialGuid = Guid.NewGuid();
            string uniqueSerial = serialGuid.ToString("N");

            string uniqueSerialLength = uniqueSerial.Substring(0, 28).ToUpper();

            char[] serialArray = uniqueSerialLength.ToCharArray();
            var finalSerialNumber = string.Empty;

            for (int i = 0; i < 28; i++)
            {
                int j;
                for (j = i; j < 4 + i; j++)
                {
                    finalSerialNumber += serialArray[j];
                }

                if (j == 28)
                {
                    break;
                }
                else
                {
                    i = j - 1;
                    finalSerialNumber += "-";
                }
            }

            return finalSerialNumber;
        }

        /// <summary>
        /// Gets the mac address From System.Net namespace.
        /// </summary>
        /// <returns>MAC address string</returns>
        public static string GetMacAddress()
        {
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                string macAddress = string.Empty;
                foreach (NetworkInterface adapter in nics)
                {
                    if (macAddress == String.Empty)
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        macAddress = adapter.GetPhysicalAddress().ToString();
                    }
                }

                return macAddress;
            }
            catch (Exception)
            {
                return "NA";
            }
        }

        /// <summary>
        /// Images to byte array.
        /// </summary>
        /// <param name="imageIn">The image in.</param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(HttpPostedFileBase imageIn)
        {
            MemoryStream target = new MemoryStream();
            imageIn.InputStream.CopyTo(target);
            return target.ToArray();
        }

        /// <summary>
        /// Bytes the array to image.
        /// </summary>
        /// <param name="byteArrayIn">The byte array in.</param>
        /// <returns></returns>
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        /// <summary>
        /// Extracts from string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public static List<string> ExtractFromString(string text, string start, string end)
        {
            List<string> matched = new List<string>();
            int index_start = 0, index_end = 0;
            bool exit = false;
            while (!exit)
            {
                index_start = text.IndexOf(start);
                index_end = text.IndexOf(end);
                if (index_start != -1 && index_end != -1)
                {
                    matched.Add(text.Substring(index_start + start.Length, index_end - index_start - start.Length));
                    text = text.Substring(index_end + end.Length);
                }
                else
                {
                    exit = true;
                }
            }

            return matched;
        }

        /// <summary>
        /// Makes the thumbnail.
        /// </summary>
        /// <param name="myImage">My image.</param>
        /// <param name="thumbWidth">Width of the thumb.</param>
        /// <param name="thumbHeight">Height of the thumb.</param>
        /// <returns></returns>
        public static byte[] MakeThumbnailFromImage(byte[] myImage, int thumbWidth, int thumbHeight)
        {
            using (MemoryStream ms = new MemoryStream())
            using (Image thumbnail = Image.FromStream(new MemoryStream(myImage)).GetThumbnailImage(thumbWidth, thumbHeight, null, new IntPtr()))
            {
                thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// return Base64 string for Image
        /// </summary>
        /// <param name="myImage"></param>
        /// <param name="thumbWidth"></param>
        /// <param name="thumbHeight"></param>
        /// <returns></returns>
        public static string MakeThumbnailFromImage(Stream myImage, int thumbWidth, int thumbHeight)
        {
            using (MemoryStream ms = new MemoryStream())
            using (Image thumbnail = Image.FromStream(myImage).GetThumbnailImage(thumbWidth, thumbHeight, null, new IntPtr()))
            {
                thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// <summary>
        /// Generates the password.
        /// </summary>
        /// <returns></returns>
        public static string GeneratePassword()
        {
            string strPwdchar = "abcdefghijklmnopqrstuvwxyz0123456789#+@&$ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string strPwd = "";
            Random rnd = new Random();
            for (int i = 0; i <= 7; i++)
            {
                int irandom = rnd.Next(0, strPwdchar.Length - 1);
                strPwd += strPwdchar.Substring(irandom, 1);
            }

            return strPwd;
        }
    }
}