namespace WebNovelConverter.Sources.Models
{
    public class WebNovelChapter
    {
        public string Url { get; set; }

        public int ChapterId { get; set; }

        public string ChapterName { get; set; }

        public string NextChapterUrl { get; set; }

        public string Content { get; set; }

        public override string ToString()
        {
            return ChapterName;
        }
    }
}
