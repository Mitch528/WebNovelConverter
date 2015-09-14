using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebNovelConverter.Sources
{
    public abstract class WebNovelSource
    {
        public abstract Task<ChapterLink[]> GetLinks(string baseUrl);
        
        public abstract Task<WebNovelChapter> GetChapterAsync(ChapterLink link);

        protected async Task<string> GetWebPage(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var resp = await client.GetAsync(url);
                resp.EnsureSuccessStatusCode();

                byte[] content = await resp.Content.ReadAsByteArrayAsync();

                return Encoding.UTF8.GetString(content, 0, content.Length);
            }
        }
    }
}
