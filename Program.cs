using System;

using System.Collections.Generic;

using System.Text;

using System.Net;
using System.Web;
using System.IO;
using System.Threading;
using TJ;

namespace thief
{
    class Program
    {
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

