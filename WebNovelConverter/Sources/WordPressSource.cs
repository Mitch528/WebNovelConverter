using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;

namespace WebNovelConverter.Sources
{
    public class WordPressSource : WebNovelSource
    {
        private static readonly string[] BloatClasses =
        {
            "sharedaddy",
            "share-story-container",
            "code-block",
            "comments-area"
        };

        private static readonly string[] PageClasses =
        {
            "entry-content",
            "post-content",
            "the-content",
            "entry",
            "post-entry"
        };

        private static readonly string[] PostClasses =
        {
            "post-entry",
            "entry-content",
            "post-content",
            "postbody"
        };

        public WordPressSource() : base("WordPress")
        {
        }

        public override async Task<IEnumerable<ChapterLink>> GetChapterLinksAsync(string baseUrl, CancellationToken token = default(CancellationToken))
        {
            string baseContent = await GetWebPageAsync(baseUrl, token);

            IHtmlDocument doc = await Parser.ParseAsync(baseContent, token);

            var divElements = from e in doc.All
                              where e.LocalName == "div"
                              where e.HasAttribute("class")
                              let names = e.GetAttribute("class").Split(' ')
                              from name in names
                              where PageClasses.Any(p => p.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                              select e;

            IElement element = divElements.FirstOrDefault();

            if (element == null)
                return EmptyLinks;

            return CollectChapterLinks(baseUrl, element.Descendents<IElement>());
        }

        public override async Task<WebNovelChapter> GetChapterAsync(ChapterLink link, CancellationToken token = default(CancellationToken))
        {
            string pageContent = await GetWebPageAsync(link.Url, token);

            IHtmlDocument doc = await Parser.ParseAsync(pageContent, token);
            IElement articleElement = doc.All.FirstOrDefault(p => p.LocalName == "article");

            string content;
            if (articleElement != null)
            {
                IElement ifElement = articleElement.Descendents<IElement>().FirstOrDefault(p => p.LocalName == "iframe");

                if (ifElement != null && ifElement.HasAttribute("src"))
                {
                    string ifUrl = ifElement.GetAttribute("src");

                    content = await GetWebPageAsync(ifUrl, token);
                }
                else
                {
                    RemoveBloat(articleElement);

                    content = articleElement.InnerHtml;
                }
            }
            else
            {
                IElement contentElement = (from e in doc.All
                                           where e.HasAttribute("class")
                                           let names = e.GetAttribute("class").Split(' ')
                                           from name in names
                                           where PostClasses.Any(p => p.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                                           select e).FirstOrDefault();

                if (contentElement != null)
                {
                    if (contentElement.LocalName == "iframe")
                    {
                        content = contentElement.InnerHtml;
                    }
                    else
                    {
                        RemoveBloat(contentElement);

                        content = contentElement.OuterHtml;
                    }
                }
                else
                {
                    content = "No Content";
                }
            }

            return new WebNovelChapter
            {
                Url = link.Url,
                Content = content
            };
        }

        public override Task<string> GetNovelCoverAsync(string baseUrl, CancellationToken token = new CancellationToken())
        {
            return Task.FromResult(string.Empty);
        }

        protected virtual void RemoveBloat(IElement element)
        {
            var shareElements = (from e in element.Descendents<IElement>()
                                 where e.HasAttribute("class")
                                 let names = e.GetAttribute("class").Split(' ')
                                 from name in names
                                 where BloatClasses.Any(p => p.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                                 select e).Distinct().ToList();

            foreach (IElement e in shareElements)
            {
                e.Remove();
            }
        }
    }
}
