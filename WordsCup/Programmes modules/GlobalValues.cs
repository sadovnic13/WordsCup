using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HtmlAgilityPack;
using WordsCup.DB;

namespace WordsCup
{
    public class User
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int balance { get; set; }
        public string saveWord { get; set; }

        public int successPoint { get; set; }
    }

    class GlobalValues
    {
        public static User user;        

        public static string url = "https://studfile.net/preview/";

        public static string doc {  get; set; }
        public static string tempDoc {  get; set; }
        
        public static string userDefinedText { get; set; }

    }
}
