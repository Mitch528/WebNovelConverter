using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using WebNovelConverter.Sources.Models;
using System.Text.RegularExpressions;

namespace WebNovelConverter.Sources
{
    public class RoyalRoadLSource : WebNovelSource
    {
        public override string BaseUrl => "http://royalroadl.com/";

        public RoyalRoadLSource() : base("RoyalRoadL")
        {
        }

        public override async Task<IEnumerable<ChapterLink>> GetChapterLinksAsync(string baseUrl, CancellationToken token = default(CancellationToken))
        {
            string baseContent = await GetWebPageAsync(baseUrl, token);

            IHtmlDocument doc = await Parser.ParseAsync(baseContent, token);

            var chapterElements = from element in doc.All
                                  where element.LocalName == "li"
                                  where element.HasAttribute("class")
                                  let classAttrib = element.GetAttribute("class")
                                  where classAttrib.Contains("chapter")
                                  select element;

            return CollectChapterLinks(baseUrl, chapterElements);
        }

        protected override IEnumerable<ChapterLink> CollectChapterLinks(string baseUrl, IEnumerable<IElement> linkElements, Func<IElement, bool> linkFilter = null)
        {
            foreach (IElement chapterElement in linkElements)
            {
                IElement linkElement = chapterElement.Descendents<IElement>().FirstOrDefault(p => p.LocalName == "a");

                if (linkElement == null || !linkElement.HasAttribute("title") || !linkElement.HasAttribute("href"))
                    continue;

                string title = linkElement.GetAttribute("title");

                ChapterLink link = new ChapterLink
                {
                    Name = title,
                    Url = linkElement.GetAttribute("href"),
                    Unknown = false
                };

                yield return link;
            }
        }

        public override async Task<WebNovelChapter> GetChapterAsync(ChapterLink link, 
            ChapterRetrievalOptions options = default(ChapterRetrievalOptions),
            CancellationToken token = default(CancellationToken))
        {
            string pageContent = await GetWebPageAsync(link.Url, token);
            
            IHtmlDocument doc = await Parser.ParseAsync(pageContent, token);
            
            IElement postBodyEl = (from e in doc.All
                                        where e.LocalName == "div"
                                        where e.HasAttribute("class")
                                        let classAttribute = e.GetAttribute("class")
                                        where classAttribute.Contains("post_body")
                                        select e).FirstOrDefault();

            if (postBodyEl == null)
                return null;

            RemoveNavigation(postBodyEl);
            RemoveDonation(postBodyEl);
            ExpandSpoilers(postBodyEl);
            RemoveEmpyTags(postBodyEl);

            var content = CleanupHTML(postBodyEl.InnerHtml);

            return new WebNovelChapter
            {
                Url = link.Url,
                Content = content
            };
        }

        public override async Task<WebNovelInfo> GetNovelInfoAsync(string baseUrl, CancellationToken token = default(CancellationToken))
        {
            string baseContent = await GetWebPageAsync(baseUrl, token);

            IHtmlDocument doc = await Parser.ParseAsync(baseContent, token);

            var fictionHeaderDes = doc.GetElementById("fiction-header");
            var coverUrl = fictionHeaderDes.Descendents<IElement>().FirstOrDefault(p => p.LocalName == "img")?.GetAttribute("src");
            var title = fictionHeaderDes.QuerySelector("h1.fiction-title")?.TextContent;

            return new WebNovelInfo()
            {
                CoverUrl = coverUrl,
                Title = title
            };
        }

        protected virtual void RemoveNavigation(IElement rootElement)
        {
            // Last 1-2 tables might be navigation

            foreach(var table in rootElement.QuerySelectorAll("table").Reverse().Take(2))
            {
                if( table.QuerySelectorAll("a").Any(x => x.TextContent.Contains("Chapter"))) {
                    table.Remove();
                }
            }
        }

        protected virtual void RemoveDonation(IElement rootElement)
        {
            foreach (var el in rootElement.QuerySelectorAll("div.thead"))
            {
                if (el.TextContent.Contains("Donation for the Author"))
                    el.Remove();
            }
        }

        /// <summary>
        /// Expands spoilers in HTML for easy reading.
        /// Expects:
        ///     <div class="spoiler_header">Spoilerxxx</div>
        ///     <div class="spoiler_body" style="display: none;">xxxx</div>
        /// </summary>
        /// <param name="rootElement"></param>
        protected void ExpandSpoilers(IElement rootElement)
        {
            foreach(var el in rootElement.QuerySelectorAll(".spoiler_body"))
            {
                el.SetAttribute("style", string.Empty);
                el.SetAttribute("class", string.Empty);

            }

            foreach (var el in rootElement.QuerySelectorAll(".spoiler_header"))
            {
                el.Remove();
            }
        }

        private void RemoveEmpyTags(IElement rootElement)
        {
            foreach (var el in rootElement.QuerySelectorAll("div,span"))
            {
                if (string.IsNullOrWhiteSpace(el.TextContent) && el.ChildElementCount == 0)
                {
                    el.Remove();
                }
            }
        }

        private string CleanupHTML(string html)
        {
            // Too many newlines sometimes
            html = new Regex("(<br>\\s*){3,}").Replace(html, "<br /><br />");

            return html.Trim();
        }
    }
}
