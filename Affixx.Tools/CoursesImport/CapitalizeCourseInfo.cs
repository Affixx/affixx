using Affixx.Core.Database;
using Affixx.Core.Database.Generated;
using Affixx.Core.Logging;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Affixx.Tools.CoursesImport
{
    public class CapitalizeCourseInfo : ITool

    {
        private readonly string inputFolderPath;
        private readonly string outputFolderPath;

        public CapitalizeCourseInfo(string[] args)
        {
            if (args == null || args.Length < 2)
            {
                throw new ArgumentException("Usage: Affixx.Tools.exe <path-to-input-folder> <path-to-output>", nameof(args));
            }

            inputFolderPath = args[0];
            outputFolderPath = args[1];
        }
        public void Execute()
        {
            var files = Directory.EnumerateFiles(inputFolderPath).Where(x => x.EndsWith(".csv"));
            var configuration = new CsvConfiguration { HasHeaderRecord = true, IgnoreHeaderWhiteSpace = true };

            foreach (var file in files)
            {
                var list = new List<CourseInfo>();
                using (var streamReader = new StreamReader(file))
                {
                    using (var reader = new CsvReader(streamReader, configuration))
                    {
                        foreach (var record in reader.GetRecords<CourseInfo>())
                        {
                            record.CourseName = ToTitleCase(record.CourseName);
                            list.Add(record);
                        }
                    }
                }

                var fileName = Path.GetFileName(file);
                var outputFile = Path.Combine(outputFolderPath, fileName);
                using (var streamWriter = new StreamWriter(outputFile))
                {
                    using (var writer = new CsvWriter(streamWriter, configuration))
                    {
                        writer.WriteHeader<CourseInfo>();
                        foreach (var item in list)
                        {
                            writer.WriteRecord(item);
                        }
                    }
                }
            }

        }
        private string ToTitleCase(string s)
        {
            if (s == null) return s;

            var words = s.Split(new[] { ' ' },  StringSplitOptions.RemoveEmptyEntries);
            var lowercaseWords = new[] {"to","and", "in", "or", "of", "the", "a", "an", "through", "with", "within", "as", "for", "on", "our", "after", "from"}; 
            for (int i = 0; i < words.Length; i++)
            {
                if (i>0 && lowercaseWords.Any(x => string.Equals(x, words[i], StringComparison.InvariantCultureIgnoreCase))) {
                    words[i] = words[i].ToLower();
                    continue;
                }
                Char firstChar = Char.ToUpper(words[i][0]);
                String rest = "";
                if (words[i].Length > 1)
                {
                    rest = words[i].Substring(1);
                }
                words[i] = firstChar + rest;
            }
            return String.Join(" ", words);
        }
        public class CourseInfo
        {
            public string Group { get; set; }
            public string CourseName { get; set; }
            public string CourseCode { get; set; }
        }
    }

}
