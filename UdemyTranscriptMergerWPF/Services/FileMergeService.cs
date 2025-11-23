using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UdemyTranscriptMergerWPF.Services
{
    class FileMergeService
    {
        public static List<(string Title, string Content)> BuildSections(List<string> filePaths)
        {
            var output = new List<(string Title, string Content)>();

            for (int i = 0; i < filePaths.Count; i++)
            {
                string path = filePaths[i];
                string name = Path.GetFileNameWithoutExtension(path);

                string title = $"BÖLÜM {i + 1}: {name}";
                string content = File.ReadAllText(path);

                output.Add((title, content));
            }

            return output;
        }
    }
}
