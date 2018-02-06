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

        public static async Task<bool> SendRequestAsync3(string endpoint, string post = null, bool login = false)
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
                request.Accept = "gzip,deflate*";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                ((NameValueCollection)request.Headers)["Cookie2"] = "$Version=1";
                ((NameValueCollection)request.Headers)["Accept-Language"] = "en-US,en;q=0.9,he-IL;q=0.8,he;q=0.7";
                ((NameValueCollection)request.Headers)["X-IG-App-ID"] = "567067343352427";
                ((NameValueCollection)request.Headers)["X-IG-Capabilities"] = "3brDAw==";
                ((NameValueCollection)request.Headers)["X-IG-Connection-Type"] = "WIFI";
                ((NameValueCollection)request.Headers)["X-IG-Connection-Speed"] = "2000kbps";
                ((NameValueCollection)request.Headers)["X-IG-Bandwidth-Speed-KBPS"] = "-1.00";
                ((NameValueCollection)request.Headers)["X-IG-Bandwidth-TotalBytes-B"] = "0";
                ((NameValueCollection)request.Headers)["X-IG-Bandwidth-TotalTime-M"] = "0";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";//InstaInfo.UserAgent;
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
