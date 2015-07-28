using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNovelConverter
{
    public class WebNovelChapter
    {
        public string Url { get; set; }

        public int ChapterId { get; set; }

        public string ChapterName { get; set; }

        public string Content { get; set; }
    }
}
