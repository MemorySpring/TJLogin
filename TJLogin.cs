using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
/// <summary>
/// Made by Dale 2016.11.25
/// Tongji-University
/// Email：1847986694@qq.com
/// </summary>
namespace  TJ
{
    public class TJLogin
    {
        static CookieContainer cookie = new CookieContainer();
        public static string GetContent(CookieContainer cookie1, string url)
        {
            string content;
            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpRequest.CookieContainer = cookie;
            httpRequest.Referer = url;
            httpRequest.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 46.0.2486.0 Safari / 537.36 Edge / 13.10586";
            httpRequest.Accept = "text / html, */*";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Method = "GET";
            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (Stream responsestream = httpResponse.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(responsestream, System.Text.Encoding.UTF8))
                {
                    content = sr.ReadToEnd();
                }
            }
            return content;
        }

        /// <summary>
        /// 以指定cookie访问url获取内容
        /// </summary>
        /// <param name="cookie1"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetWebContent(CookieContainer cookie1, string url)
        {
            string content;
            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpRequest.CookieContainer = cookie1;
            httpRequest.Referer = url;
            httpRequest.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 46.0.2486.0 Safari / 537.36 Edge / 13.10586";
            httpRequest.Accept = "text / html, */*";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Method = "GET";
            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (Stream responsestream = httpResponse.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(responsestream, System.Text.Encoding.Default))
                {
                    content = sr.ReadToEnd();
                }
            }
            return content;
        }

        /// <summary>
        /// 携带cookie以post方式向指定网站发送数据后更新cookie
        /// </summary>
        /// <param name="cookie1"></param>
        /// <param name="poststring"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static CookieContainer GetCookieByCookie(CookieContainer cookie1, String poststring, string url)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpRequest.CookieContainer = cookie;
            httpRequest.Referer = url;
            httpRequest.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 46.0.2486.0 Safari / 537.36 Edge / 13.10586";
            httpRequest.Accept = "text / html, */*";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Method = "POST";
            httpRequest.KeepAlive = true;
            byte[] bytes = System.Text.Encoding.Default.GetBytes(poststring);
            httpRequest.ContentLength = bytes.Length;
            Stream stream = httpRequest.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();//以上是POST数据的写入
            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            httpResponse.Close();
            return cookie;
        }   

        /// <summary>
       /// 向指定url以post方式一段数据后 获得cookie
       /// </summary>
       /// <param name="postString">将post的数据</param>
       /// <param name="postUrl">post目标url</param>
       /// <returns></returns>
        public  static CookieContainer GetCookie(string postString, string postUrl)
        {
            CookieContainer cookie1 = new CookieContainer();
            HttpWebRequest httpRequset = (HttpWebRequest)HttpWebRequest.Create(postUrl);//创建http 请求
            httpRequset.CookieContainer = cookie;//设置cookie
            httpRequset.Method = "POST";//POST 提交
            httpRequset.KeepAlive = true;
            httpRequset.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 46.0.2486.0 Safari / 537.36 Edge / 13.10586";
            httpRequset.Accept = "text / html, */*";
            httpRequset.ContentType = "application/x-www-form-urlencoded";//以上信息在监听请求的时候都有的直接复制过来
            byte[] bytes = System.Text.Encoding.Default.GetBytes(postString);
            httpRequset.ContentLength = bytes.Length;
            Stream stream = httpRequset.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();//以上是POST数据的写入
            HttpWebResponse httpResponse = (HttpWebResponse)httpRequset.GetResponse();//获得 服务端响应
            httpResponse.Close();
            return cookie;//拿到cookie
        }


        /// <summary>
        /// 获取同济SSO登录方式的cookie
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>cookie</returns>
        public static CookieContainer Login4m3(String username,String password)
        {
            String posts = "option=credential&Ecom_User_ID=" + username + "&Ecom_Password=" + password + "&submit=%E7%99%BB%E5%BD%95";
            cookie = new CookieContainer();
            String s = GetContent(cookie, "http://4m3.tongji.edu.cn/eams/samlCheck");
            s = s.Substring(s.IndexOf("url=") + 4);
            s = s.Substring(0, s.IndexOf("\""));
            s = GetContent(cookie, s);
            s = s.Substring(s.IndexOf("action=") + 8);
            s = s.Substring(0, s.IndexOf("\""));
            s = "https://ids.tongji.edu.cn:8443" + s;
            cookie = GetCookie("", s);
            cookie = GetCookieByCookie(cookie, posts, "https://ids.tongji.edu.cn:8443/nidp/app/login?sid=0&sid=0");
            s = GetContent(cookie, "https://ids.tongji.edu.cn:8443/nidp/saml2/sso?sid=0");
            String p1, p2;
            p1 = s.Substring(s.IndexOf("value=") + 7);
            p2 = p1.Substring(p1.IndexOf("value=") + 7);
            p1 = p1.Substring(0, p1.IndexOf("\""));
            p2 = p2.Substring(0, p2.IndexOf("\""));
            p1 = p1.Replace("+", "%2B");
            p1 = p1.Replace("=", "%3D");
            p1 = "SAMLResponse=" + p1 + "&RelayState=" + p2;
            try
            {
                GetCookieByCookie(cookie, p1, "http://4m3.tongji.edu.cn/eams/saml/SAMLAssertionConsumer");
                return cookie;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取同济tjjs方式登录的cookie
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>cookie</returns>
        public static CookieContainer LoginXuanke(String username ,String password)
        {
            String data = "IDToken0=&IDToken1=" + username + "&IDToken2=" + password + "&IDButton=Submit&goto=null&gx_charset=UTF-8";
            CookieContainer cookie = new CookieContainer();
            cookie = GetCookie(data, "http://tjis.tongji.edu.cn:58080/amserver/UI/Login");
            cookie= GetCookieByCookie(cookie,"", "http://xuanke.tongji.edu.cn/pass.jsp");
            String s = GetContent(cookie,"http://xuanke.tongji.edu.cn/tj_login/frame.jsp");
            if (s.IndexOf("frame") != -1)
            {
                return cookie;
            }
            else
            {
                return null; 
            }
        }
    }
}
