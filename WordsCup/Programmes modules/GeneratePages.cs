using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace WordsCup.Programmes_modules
{
    class GeneratePages
    {
        public static async Task<HtmlDocument> GeneratePage()
        {
            try
            {
                Random rnd = new Random();
                int num = rnd.Next(3393, 20020019);
                int page = rnd.Next(2, 15);

                //return await Task.Run(() => new HtmlWeb().Load("https://studfile.net/preview/406275/"));
                return await Task.Run(() => new HtmlWeb().Load(GlobalValues.url + num + "/page:" + page));
            }
            catch (WebException)
            {
                return null;
            }
        }


        public static async Task<String> ViewTextBrowser()
        {
            HtmlDocument doc = null;

            try
            {
                var htmlContent = await Task.Run(async () =>
                {
                    HtmlNode bodyContent;
                    while (true)
                    {
                        doc = await GeneratePage();
                        if (doc == null)
                        {
                            return null;
                        }

                        bodyContent = doc.DocumentNode.SelectSingleNode("//div[@class='pdf_holder']");

                        if (bodyContent != null)
                        {
                            break;
                        }
                    }
                    var nodes = bodyContent.ChildNodes;

                    return string.Join("\n", nodes.Select(node => node.OuterHtml));
                });

                string html = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset='UTF-8'>
                    </head>
                    <body>
                        {htmlContent}
                    </body>
                    </html>";

                return html;
            }

            catch (NullReferenceException)
            {
                return await ViewTextBrowser();
            }

        }

    }
}
