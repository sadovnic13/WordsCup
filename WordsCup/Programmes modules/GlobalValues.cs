using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WordsCup
{
    class GlobalValues
    {
        public static string url = "https://ru.wikipedia.org/wiki/Special:Random";
        public static HtmlDocument doc {  get; set; }
        
        public static string userDefinedText { get; set; }

        public static void GeneratePage()
        {            
            GlobalValues.doc = new HtmlWeb().Load(url);
        }
    }
}
