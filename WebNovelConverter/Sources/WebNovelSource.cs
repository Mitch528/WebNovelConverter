using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Parser.Html;

namespace WebNovelConverter.Sources
{
    public class WebNovelSource
    {
        protected static readonly ChapterLink[] EmptyLinks = new ChapterLink[0];

        public virtual string BaseUrl { get; protected set; }

        public string SourceName { get; private set; }

        protected readonly HtmlParser Parser = new HtmlParser();

        public WebNovelSource(string sourceName)
        {
            SourceName = sourceName;
        }

        public virtual Task<IEnumerable<ChapterLink>> GetChapterLinksAsync(string baseUrl, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public virtual Task<WebNovelChapter> GetChapterAsync(ChapterLink link, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public virtual Task<string> GetNovelCoverAsync(string baseUrl, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        protected virtual IEnumerable<ChapterLink> CollectChapterLinks(string baseUrl, IEnumerable<IElement> linkElements,
            Func<IElement, bool> linkFilter = null)
        {
            if (linkFilter != null)
                linkElements = linkElements.Where(linkFilter);

            linkElements = linkElements.Where(p => p.LocalName == "a");

            foreach (IElement e in linkElements)
            {
                if (string.IsNullOrWhiteSpace(e.TextContent) || !e.HasAttribute("href"))
                    continue;

                string url = UrlHelper.ToAbsoluteUrl(baseUrl, e.GetAttribute("href"));

                if (string.IsNullOrEmpty(url))
                    continue;

                ChapterLink link = new ChapterLink
                {
                    Name = WebUtility.HtmlDecode(e.TextContent),
                    Url = url
                };

                yield return link;
            }
        }

        protected async Task<string> GetWebPageAsync(string url, CancellationToken token = default(CancellationToken))
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("WebNovelConverter", "1.0"));

                UriBuilder uriBuilder = new UriBuilder(url);

                var resp = await client.GetAsync(uriBuilder.Uri, token);
                resp.EnsureSuccessStatusCode();

                byte[] content = await resp.Content.ReadAsByteArrayAsync();

                return Encoding.UTF8.GetString(content, 0, content.Length);
            }
        }
    }
}
