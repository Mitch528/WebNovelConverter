using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNovelConverter
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
