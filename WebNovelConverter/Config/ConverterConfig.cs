using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebNovelConverter.Config
{
    [JsonObject]
    public class ConverterConfig
    {
        public int DelayPerChapterSeconds { get; set; }

        public ConverterConfig()
        {
            DelayPerChapterSeconds = 5;
        }
    }
}
