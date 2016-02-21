using System;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Extensions;

namespace WebNovelConverter.Sources
{
    public class WuxiaWorldSource : WordPressSource
    {
        public override string BaseUrl => "http://wuxiaworld.com/";

        private static readonly string[] LinksToRemove =
        {
            "Next Chapter",
            "Previous Chapter"
        };

        public WuxiaWorldSource() : base("WuxiaWorld")
        {
        }

        protected override void RemoveBloat(IElement element)
        {
            base.RemoveBloat(element);

            var toRemove = (from e in element.Descendents<IElement>()
                            where e.LocalName == "a"
                            where LinksToRemove.Any(x => x == e.Text().Trim())
                            select e).ToList();

            foreach (IElement e in toRemove)
            {
                e.Remove();
            }
        }
    }
}
