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

        public static HtmlDocument doc {  get; set; }
        
        public static string userDefinedText { get; set; }

        public static void GeneratePage()
        {
            try
            {
                Random rnd = new Random();
                int num = rnd.Next(3393, 20020019);
                int page = rnd.Next(2, 15);

                GlobalValues.doc = new HtmlWeb().Load(url + num + "/page:" + page);
                //GlobalValues.doc = new HtmlWeb().Load("");

     //           var nodesToRemove = GlobalValues.doc.DocumentNode.DescendantsAndSelf()
     //.Where(n => n.NodeType == HtmlNodeType.Element && (n.Name == "a" || n.Name == "img" || n.Name == "figcaption"))
     //.ToList();
     //           foreach (var node in GlobalValues.doc.DocumentNode.DescendantsAndSelf())
     //           {
     //               if (node.NodeType == HtmlNodeType.Element)
     //               {
     //                   node.Attributes.Remove("class");
     //               }
     //           }

     //           foreach (var node in nodesToRemove)
     //           {
     //               node.Remove();
     //           }



                //foreach (var link in doc.DocumentNode.DescendantsAndSelf("a"))
                //{
                //    link.Attributes.Remove("href");
                //}
            }
            catch (WebException e)
            {
                MessageBox.Show(e.Message);
            }
        }


    }
}
