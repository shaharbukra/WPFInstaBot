using InstaBot.Objects;
using System;
using System.IO;
using System.Net;

namespace InstaBot.Helpers
{
    internal class GenerateData
    {
        private static Random random = new Random();
        internal static string UUID(bool type)
        {
            string uuid = string.Format("{0:x4}{1:x4}-{2:x4}-{3:x4}-{4:x4}-{5:x4}{6:x4}{7:x4}", random.Next(0, 65535), random.Next(0, 65535), random.Next(0, 65535), random.Next(0, 4095) | 0x4000, random.Next(0, 16383) | 0x8000, random.Next(0, 65535), random.Next(0, 65535), random.Next(0, 65535));
            if (!type)
            {
                return uuid.Replace("-", "");
            }
            return uuid;
        }

        internal static string DeviceId()
        {
            string seed = Hash.CalculateMD5Hash(InstaInfo.Login + InstaInfo.Password);
            DateTime volatile_seed = File.GetLastWriteTime(Environment.CurrentDirectory);
            return "android-" + Hash.CalculateMD5Hash(seed + volatile_seed).Substring(16);
        }

        internal static string Signature(string data)
        {
            string hash = BitConverter.ToString(Helpers.Hash.HmacSHA256(data, InstaInfo.IgSigKey)).Replace("-", "").ToLower();
            return "ig_sig_key_version=" + InstaInfo.SigKeyVersion + "&signed_body=" + hash + "." + WebUtility.UrlEncode(data);
        }
    }
}