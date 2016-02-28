using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using WebNovelConverter.Extensions;
using WebNovelConverter.Sources.Models;

namespace WebNovelConverter.Sources
{
    public class BlogspotSource : WebNovelSource
    {
        protected readonly List<string> PostClasses = new List<string>
        {
            "pagepost",
            "post-body",
            "entry-content",
            "post-outer",
            "main-content",
            "post"
        };

        protected readonly List<string> TitleClasses = new List<string>
        {
            "post-title",
            "entry-title"
        };

        protected readonly List<string> NextChapterNames = new List<string>
        {
            "Next Chapter"
        };

        protected readonly List<string> BloatClasses = new List<string>
        {
            "post-footer"
        };

        public BlogspotSource() : base("Blogspot")
        {
        }

        protected BlogspotSource(string sourceName) : base(sourceName)
        {
        }

        public override async Task<IEnumerable<ChapterLink>> GetChapterLinksAsync(string baseUrl, CancellationToken token = default(CancellationToken))
        {
            string baseContent = await GetWebPageAsync(baseUrl, token);

            IHtmlDocument doc = await Parser.ParseAsync(baseContent, token);

            IElement contentElement = (from e in doc.Descendents<IElement>()
                                       where e.LocalName == "div"
                                       where e.HasAttribute("class")
                                       let names = e.GetAttribute("class").Split(' ')
                                       from cl in PostClasses
                                       where names.Any(p => p.IndexOf(cl, StringComparison.OrdinalIgnoreCase) >= 0)
                                       select e).FirstOrDefault();

            if (contentElement == null)
                return new List<ChapterLink>();

            return CollectChapterLinks(baseUrl, contentElement.Descendents<IElement>());
        }

        public override async Task<WebNovelChapter> GetChapterAsync(
            ChapterLink link,
            ChapterRetrievalOptions options = default(ChapterRetrievalOptions),
            CancellationToken token = default(CancellationToken))
        {
            string baseContent = await GetWebPageAsync(link.Url, token);

            IHtmlDocument doc = await Parser.ParseAsync(baseContent, token);

            IElement titleElement = doc.DocumentElement.FirstWhereHasClass(TitleClasses);

            WebNovelChapter chapter = ParseChapter(doc.DocumentElement, token);
            chapter.Url = link.Url;

            if (titleElement != null)
                chapter.ChapterName = titleElement.Text().Trim();

            return chapter;
        }

        protected virtual WebNovelChapter ParseChapter(IElement rootElement, CancellationToken token = default(CancellationToken))
        {
            WebNovelChapter chapter = new WebNovelChapter();

            IElement element = rootElement.WhereHasClass(PostClasses).LastOrDefault()
                ?? rootElement.Descendents<IElement>().FirstOrDefault(p => p.LocalName == "article");

            IElement nextChapterElement = (from e in rootElement?.Descendents<IElement>() ?? new List<IElement>()
                                           where e.LocalName == "a"
                                           let text = e.Text()
                                           from name in NextChapterNames
                                           where text.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0
                                           select e).FirstOrDefault();

            if (nextChapterElement != null)
            {
                chapter.NextChapterUrl = nextChapterElement.GetAttribute("href");
            }

            if (element != null)
            {
                RemoveBloat(element);

                chapter.Content = element.InnerHtml;
            }
            else
            {
                chapter.Content = "No Content";
            }

            return chapter;
        }

        protected virtual void RemoveBloat(IElement element)
        {
            var shareElements = element.WhereHasClass(BloatClasses);

            foreach (IElement e in shareElements)
            {
                e.Remove();
            }
        }
    }
}
