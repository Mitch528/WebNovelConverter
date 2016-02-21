namespace WebNovelConverter.Sources.Models
{
    public struct ChapterLink
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public bool Unknown { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
