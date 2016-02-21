using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNovelConverter.Sources
{
    public class NovelSourceCollection : ICollection<WebNovelSource>
    {
        private readonly List<WebNovelSource> _sources;

        public NovelSourceCollection()
        {
            _sources = new List<WebNovelSource>();
        }

        public IEnumerator<WebNovelSource> GetEnumerator()
        {
            return _sources.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(WebNovelSource item)
        {
            _sources.Add(item);
        }

        public void Clear()
        {
            _sources.Clear();
        }

        public bool Contains(WebNovelSource item)
        {
            return _sources.Contains(item);
        }

        public WebNovelSource Get(string sourceUrl)
        {
            Uri uri;
            if (!Uri.TryCreate(sourceUrl.Replace("www.", string.Empty), UriKind.Absolute, out uri))
                return null;

            uri = new Uri(uri.GetLeftPart(UriPartial.Authority));
            
            return _sources.Where(p => !string.IsNullOrEmpty(p.BaseUrl))
                .SingleOrDefault(p => new UriBuilder(p.BaseUrl.Replace("www.", string.Empty)).Uri == uri);
        }

        public WebNovelSource GetByName(string name)
        {
            return _sources.FirstOrDefault(p => p.SourceName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void CopyTo(WebNovelSource[] array, int arrayIndex)
        {
            _sources.CopyTo(array, arrayIndex);
        }

        public bool Remove(WebNovelSource item)
        {
            return _sources.Remove(item);
        }

        public int Count => _sources.Count;

        public bool IsReadOnly => false;
    }
}
