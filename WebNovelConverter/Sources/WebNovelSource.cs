using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebNovelConverter.Sources
{
    public abstract class WebNovelSource
    {
        public abstract string BaseUrl { get; }

        public abstract Task<ChapterLink[]> GetLinks(string baseUrl);
        
        public abstract Task<WebNovelChapter> GetChapterAsync(ChapterLink link);

        public abstract Task<string> GetNovelCover(string baseUrl);

        protected async Task<string> GetWebPage(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("WebNovelConverter", "1.0"));

                UriBuilder uriBuilder = new UriBuilder(url);

                var resp = await client.GetAsync(uriBuilder.Uri);
                resp.EnsureSuccessStatusCode();

                byte[] content = await resp.Content.ReadAsByteArrayAsync();

                return Encoding.UTF8.GetString(content, 0, content.Length);
            }
        }
    }
}
