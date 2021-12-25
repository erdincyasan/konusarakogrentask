using Shared.Dtos.WebsiteContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IWebsiteContent
    {
        Task<List<string>> GetTitleUrls();
        Task<List<WebsiteContentDetail>> GetContentUrl();
    }
}
