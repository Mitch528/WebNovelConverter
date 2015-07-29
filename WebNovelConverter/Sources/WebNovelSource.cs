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

        public abstract Task<List<WebNovelChapter>> GetChaptersAsync(string url, int delayPer, IProgress<string> progress);

        public abstract Task<WebNovelChapter> GetChapterAsync(string url);

        protected async Task<string> GetWebPage(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var resp = await client.GetAsync(url);
                resp.EnsureSuccessStatusCode();

                return await resp.Content.ReadAsStringAsync();
            }
        }
    }
}
