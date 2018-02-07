using InstaBot.Objects;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace InstaBot.Helpers
{
    internal class GenerateData
    {
        private static string CommentsList = string.Empty;
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


        internal static string Comment()
        {
            var comment = "";
            var random  = new Random(InstaInfo.DateNow);
            if (CommentsList == null)
            {
                LoadCommentsList();
            }
            if (!string.IsNullOrEmpty(CommentsList))
            {
                var c_a = CommentsList.Split(';');
                for (var i = 0; i < c_a.Length; i++)
                {
                    var t_str = c_a[i].Split(',');
                    comment += t_str[random.Next(0, t_str.Length)];
                    if (i < c_a.Length - 2)
                    {
                        comment += " ";
                    }
                }
            }
            return comment;
        }

        private static void LoadCommentsList()
        {
            try
            {
                CommentsList = File.ReadAllText(Environment.CurrentDirectory + @"\data\comments.dat");
            }
            catch
            {
                File.WriteAllText(Environment.CurrentDirectory + @"\data\comments.dat", "this,the,your,This,The,Your;photo,picture,pic,shot,snapshot;is,looks,feels,is really;great,super,good,very good,wow,WOW,cool,GREAT,magnificent,magical,very cool,stylish,so stylish,beautiful,so beautiful,so stylish,so professional,lovely,so lovely,very lovely,glorious,so glorious,very glorious,adorable,excellent,amazing;.,..,...,!,!!,!!!, :)");
            }
        }

        public static string SpliceText(string text, int lineLength)
        {
            var charCount = 0;
            var lines = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(w => (charCount += w.Length + 1) / lineLength)
                .Select(g => string.Join(" ", g));

            return String.Join("\n", lines.ToArray());
        }

    }
}