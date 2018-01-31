using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InstaBot.Helpers
{
    public class Hash
    {
        public static byte[] HmacSHA256(string data, string key)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            }
        }

        internal static string CalculateMD5Hash(string input)
        {
            MD5 mD = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = mD.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }


        internal static string Encrypt(string str, string keyCrypt)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(str), keyCrypt));
        }

        /// <summary>
        /// The Encrypt
        /// </summary>
        /// <param name="key">The <see cref="byte[]"/></param>
        /// <param name="value">The <see cref="string"/></param>
        /// <returns>The <see cref="byte[]"/></returns>
        private static byte[] Encrypt(byte[] key, string value)
        {
            ICryptoTransform Ct = Rijndael.Create().CreateEncryptor(new PasswordDeriveBytes(value, null).GetBytes(16), new byte[16]);
            MemoryStream Ms = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(Ms, Ct, CryptoStreamMode.Write);
            cryptoStream.Write(key, 0, key.Length);
            cryptoStream.FlushFinalBlock();
            byte[] Result = Ms.ToArray();
            Ms.Close();
            Ms.Dispose();
            cryptoStream.Close();
            cryptoStream.Dispose();
            Ct.Dispose();
            return Result;
        }

        /// <summary>
        /// The Decrypt
        /// </summary>
        /// <param name="str">The <see cref="string"/></param>
        /// <param name="keyCrypt">The <see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        internal static string Decrypt(string str, string keyCrypt)
        {
            try
            {
                CryptoStream cryptoStream = InternalDecrypt(Convert.FromBase64String(str), keyCrypt);
                StreamReader Sr = new StreamReader(cryptoStream);
                string Result = Sr.ReadToEnd();
                cryptoStream.Close();
                cryptoStream.Dispose();
                Sr.Close();
                Sr.Dispose();
                return Result;
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

        private static CryptoStream InternalDecrypt(byte[] key, string value)
        {
            ICryptoTransform ct = Rijndael.Create().CreateDecryptor(new PasswordDeriveBytes(value, null).GetBytes(16), new byte[16]);
            return new CryptoStream(new MemoryStream(key), ct, CryptoStreamMode.Read);
        }
    }
}
