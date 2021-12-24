using Application.Common.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{

   
    public class WebsiteContent : IWebsiteContent
    {
        private readonly IHttpClientFactory _client;

        public WebsiteContent(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<string> GetContentUrl(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _client.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await client.GetStringAsync(url);
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(content);

                var x = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/main[1]/article[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]");

            return x.InnerText;
            }
            return string.Empty;
        }
    }
}
