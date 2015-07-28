using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebNovelConverter.Sources;

namespace WebNovelConverter
{
    class Program
    {
        private static StreamWriter _swriter;

        static void Main(string[] args)
        {
            string url = string.Join(" ", args);

            if (string.IsNullOrEmpty(url))
            {
                Console.WriteLine("A URL to the web novel's TOC is required!");
                Console.ReadLine();

                return;
            }

            Console.Write("Name of web novel: ");

            string name = Console.ReadLine();

            Console.Write("Output (pdf/epub): ");

            string output = Console.ReadLine();

            _swriter = new StreamWriter("output.txt", false);

            Progress<string> progress = new Progress<string>();
            progress.ProgressChanged += (s, e) =>
            {
                Console.WriteLine(e);

                if (_swriter != null)
                {
                    _swriter.WriteLine(e);
                    _swriter.Flush();
                }
            };

            WordPress wp = new WordPress();
            var chapters = wp.GetChaptersAsync(url, progress).Result;

            string fName = string.Format("{0}.html", name);

            using (StreamWriter writer = new StreamWriter(fName))
            {
                foreach (WebNovelChapter chapter in chapters.OrderBy(p => p.ChapterId))
                {
                    writer.WriteLine(chapter.Content);
                }
            }

            _swriter.Close();
            _swriter = null;

            Console.WriteLine("Converting...");

            ProcessStartInfo psi = new ProcessStartInfo("ebook-convert.exe", string.Format("\"{0}\" \"{1}.{2}\"", fName, name, output));

            Process proc = new Process();
            proc.StartInfo = psi;

            proc.Start();
            proc.WaitForExit();

            Console.WriteLine("Done!");

            Console.ReadLine();
        }
    }
}
