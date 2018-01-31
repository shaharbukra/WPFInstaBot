﻿namespace InstaBot.Api
{
    using InstaBot.Callbacks;
    using InstaBot.Helpers;
    using InstaBot.Objects;
    using InstaBot.Objects.InstagramData;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="Actions" />
    /// </summary>
    internal class Actions
    {
        /// <summary>
        /// The Login
        /// </summary>
        /// <param name="login">The <see cref="string"/></param>
        /// <param name="password">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<UserDetail> Login(string login, string password)
        {
            InstaInfo.Login = login;
            InstaInfo.Password = password;
            InstaInfo.Uuid = GenerateData.UUID(true);
            InstaInfo.DeviceId = GenerateData.DeviceId();

            Console.WriteLine(InstaInfo.Uuid + " - " + InstaInfo.DeviceId);

            InstaInfo.CookieContainer = new CookieContainer();

            Console.WriteLine("try to login");
            if (await Request.SendRequestAsync("si/fetch_headers/?challenge_type=signup&guid=" + GenerateData.UUID(false), null, true))
            {
                Log.WriteLog("Handshake with Instagram success!");
                var postData = new Dictionary<string, string>
                {
                    { "phone_id", GenerateData.UUID(true) },
                    { "_csrftoken", InstaInfo.CsrfToken },
                    { "username", InstaInfo.Login },
                    { "guid", InstaInfo.Uuid },
                    { "device_id", InstaInfo.DeviceId },
                    { "password", InstaInfo.Password },
                    { "login_attempt_count", "0" }
                };
                var post = JsonConvert.SerializeObject(postData).ToString();

                if (await Request.SendRequestAsync("accounts/login/", GenerateData.Signature(post), true))
                {
                    InstaInfo.LoginStatus = true;
                    InstaInfo.RankToken = InstaInfo.UserNameId + "_" + InstaInfo.Uuid;


                    if (await SyncFeaturesAsync())
                    {
                        Console.WriteLine("Sync Features ok!");
                        Console.WriteLine("Get RecentActivity ok!");
                        var userInfoData = new Dictionary<string, string>()
                        {
                            {"login",InstaInfo.Login },
                            {"password",InstaInfo.Password },
                            {"username_id",InstaInfo.UserNameId },
                            {"csrftoken",InstaInfo.CsrfToken },
                            {"uuid",InstaInfo.Uuid },
                            {"device_id",InstaInfo.DeviceId },
                            {"rank_token",InstaInfo.RankToken },
                            {"mid",InstaInfo.Mid },
                            {"sessionid",InstaInfo.SessionId }
                        };
                        var userInfo = JsonConvert.SerializeObject(userInfoData).ToString();

                        new FileInfo(Environment.CurrentDirectory + "\\data\\").Directory.Create();
                        File.WriteAllText(Environment.CurrentDirectory + "\\data\\" + login + "-session.dat", Hash.Encrypt(userInfo, password));

                        if(await GetUsernameInfoAsync(InstaInfo.UserNameId))
                        {
                            var userDetail = JsonConvert.DeserializeObject<UserDetail>(InstaInfo.LastResponse);
                            if (userDetail != null)
                            {
                                return userDetail;
                            }

                        }

                        //return true;
                    }
                }
                else
                {
                    var a = "Asd";
                }

            }
            return null;
        }

        /// <summary>
        /// The Like
        /// </summary>
        /// <param name="mediaId">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> Like(string mediaId)
        {
            var likeData = new Dictionary<string, string>
                {
                    {"_uuid",InstaInfo.Uuid},
                    {"_uid",InstaInfo.UserNameId},
                    {"_csrftoken",InstaInfo.CsrfToken},
                    {"media_id",mediaId}
                };
            var data = JsonConvert.SerializeObject(likeData).ToString();
            if (await Request.SendRequestAsync("media/" + mediaId + "/like/", GenerateData.Signature(data), false))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// The Follow
        /// </summary>
        /// <param name="userId">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public async Task<bool> Follow(string userId)
        {
            var followData = new Dictionary<string, string>
            {
                {"_uuid",InstaInfo.Uuid},
                {"_uid",InstaInfo.UserNameId},
                {"user_id",userId},
                {"_csrftoken",InstaInfo.CsrfToken}
            };

            var data = JsonConvert.SerializeObject(followData).ToString();
            if (await Request.SendRequestAsync("friendships/create/" + userId + "/", GenerateData.Signature(data), false))
            {
                return true;
            }
            return false;
        }

        public static async Task<bool> GetUsernameInfoAsync(string userNameId)
        {
            if (await Request.SendRequestAsync("users/" + userNameId + "/info/", null, false))
            {
                var arg = JsonConvert.DeserializeObject<Objects.InstagramData.UserDetail>(InstaInfo.LastResponse);
                if (arg != null)
                {
                    CallBackMedia.CallbackEventHandler(arg, "profile");
                }
                return true;

            }
            return false;
        }
       

        /// <summary>
        /// The SyncFeaturesAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> SyncFeaturesAsync()
        {
            var dicUserData = new Dictionary<string, string>
            {
                { "_uuid", InstaInfo.Uuid },
                { "_uid", InstaInfo.UserNameId },
                { "id", InstaInfo.UserNameId },
                { "_csrftoken", InstaInfo.CsrfToken },
                { "experiments", InstaInfo.Experiments }
            };

            string jsonData = JsonConvert.SerializeObject(dicUserData).ToString();

            if (await Request.SendRequestAsync("qe/sync/", GenerateData.Signature(jsonData), false))
            {
                return true;
            }
            return false;
        }

    }
}