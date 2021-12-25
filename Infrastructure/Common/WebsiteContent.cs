using Application.Common.Interfaces;
using HtmlAgilityPack;
using Shared.Dtos.WebsiteContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Infrastructure.Common
{


    public class WebsiteContent : IWebsiteContent
    {
        private readonly IHttpClientFactory _client;

        private string TITLE_URL = "https://www.wired.com/sitemap.xml";
        public WebsiteContent(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<List<WebsiteContentDetail>> GetContentUrl()
        {
            var client = _client.CreateClient();
            var titleUrls = await GetTitleUrls();
            var webContentAndTitles = new List<WebsiteContentDetail>();
            foreach (string url in titleUrls)
            {
                var content = await client.GetStringAsync(url);
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(content);
                var webTitle = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/main[1]/article[1]/div[1]/header[1]/div[1]/div[1]/h1[1]");
                var webContent = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/main[1]/article[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]");
                webContentAndTitles.Add(new()
                {
                    Title = HttpUtility.HtmlDecode(webTitle.InnerText),
                    Body = HttpUtility.HtmlDecode(webContent.InnerText)
                });
            }



            return webContentAndTitles;
        }

        public async Task<List<string>> GetTitleUrls()
        {
            var client = _client.CreateClient();
            var stringSitemapLastWeek = await client.GetStringAsync(TITLE_URL);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(stringSitemapLastWeek);
            string lastWeekUrl = GetLastWeek(xmlDoc);

            List<string> contents = new List<string>();
            if (!string.IsNullOrWhiteSpace(lastWeekUrl))
            {
                var stringSitemapLastContents = await client.GetStringAsync(lastWeekUrl);
                xmlDoc.LoadXml(stringSitemapLastContents);
                XmlNodeList nodes = xmlDoc.GetElementsByTagName("url");
                int i = 0;
                foreach (XmlNode node in nodes)
                {

                    if (node["loc"] != null && i < 5)
                    {
                        contents.Add(node["loc"].InnerText);
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
                return contents;
            }

            return null;
        }

        public string GetLastWeek(XmlDocument document)
        {
            XmlNodeList xmlNodeList = document.GetElementsByTagName("sitemap");
            foreach (XmlNode node in xmlNodeList)
            {
                if (node["loc"] != null)
                {
                    return node["loc"]?.InnerText;
                }
            }
            return "";
        }
    }
}
