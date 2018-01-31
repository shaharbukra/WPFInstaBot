using InstaBot.Helpers;
using InstaBot.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace InstaBot.Api
{
//https://github.com/mgp25/Instagram-API/tree/master/src/Request

    /// <summary>
    /// Defines the <see cref="Request" />
    /// </summary>
    public class Request
    {

       

        /// <summary>
        /// The SendRequestAsync
        /// </summary>
        /// <param name="endpoint">The <see cref="string"/></param>
        /// <param name="post">The <see cref="string"/></param>
        /// <param name="login">The <see cref="bool"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> SendRequestAsync(string endpoint, string post = null, bool login = false)
        {
            Console.WriteLine("SendRequestAsync");
            try
            {
                Console.WriteLine("Try access " + InstaInfo.ApiUrl + endpoint);
                HttpWebRequest request = WebRequest.CreateHttp(InstaInfo.ApiUrl + endpoint);
                if (InstaInfo.Proxy)
                {
                    WebProxy webProxy = new WebProxy();
                    Uri uri2 = webProxy.Address = new Uri(InstaInfo.ProxyIp + ":" + InstaInfo.ProxyPort);
                    if (InstaInfo.ProxyUser != "" && InstaInfo.ProxyPassword != "")
                    {
                        webProxy.Credentials = new NetworkCredential(InstaInfo.ProxyUser, InstaInfo.ProxyPassword);
                    }
                    request.Proxy = webProxy;
                }
                request.CookieContainer = InstaInfo.CookieContainer;
                request.KeepAlive = false;
                request.Accept = "*/*";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                ((NameValueCollection)request.Headers)["Cookie2"] = "$Version=1";
                ((NameValueCollection)request.Headers)["Accept-Language"] = "en-US";
                request.UserAgent = InstaInfo.UserAgent;
                if (post != null)
                {
                    request.Method = "POST";
                    byte[] bytes = Encoding.ASCII.GetBytes(post);
                    request.ContentLength = bytes.Length;
                    Stream obj = await request.GetRequestStreamAsync();
                    obj.Write(bytes, 0, bytes.Length);
                    obj.Close();
                }
                using (HttpWebResponse httpWebResponse = (await request.GetResponseAsync()) as HttpWebResponse)
                {
                    if (login)
                    {
                        InstaInfo.CsrfToken = httpWebResponse.Cookies["csrftoken"].Value;
                    }
                    if (login && post != null)
                    {
                        InstaInfo.UserNameId = httpWebResponse.Cookies["ds_user_id"].Value;
                        InstaInfo.SessionId = httpWebResponse.Cookies["sessionid"].Value;
                    }
                    if (login && post == null)
                    {
                        InstaInfo.Mid = httpWebResponse.Cookies["mid"].Value;
                    }
                    using (Stream stream = httpWebResponse.GetResponseStream())
                    {
                        StreamReader streamReader = new StreamReader(stream);
                        string str = InstaInfo.LastResponse = streamReader.ReadToEnd();
                        if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                        {
                            httpWebResponse.Close();
                            streamReader.Close();
                            return true;
                        }
                        Log.WriteLog("Response: " + str);
                        httpWebResponse.Close();
                        streamReader.Close();
                        return false;
                    }
                }
            }
            catch (WebException ex)
            {
                Log.WriteLog(ex.Message);
                if (ex.Response != null)
                {
                    using (HttpWebResponse httpWebResponse2 = (HttpWebResponse)ex.Response)
                    {
                        using (StreamReader streamReader2 = new StreamReader(httpWebResponse2.GetResponseStream()))
                        {
                            InstaInfo.LastResponse = streamReader2.ReadToEnd();
                            Log.WriteLog(InstaInfo.LastResponse);
                        }
                    }
                }
                return false;
            }
        }
    }
}
