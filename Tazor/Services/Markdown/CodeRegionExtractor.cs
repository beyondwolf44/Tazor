using System;
using System.Collections.Generic;
using System.IO;

namespace Tazor.Services.Markdown
{
    public class CodeRegionExtractor
    {
        private readonly string _sourceRoot;

        public CodeRegionExtractor(string sourceRoot)
        {
            _sourceRoot = sourceRoot;
        }

        public string ExtractRegion(string fileLocation, string title)
        {
            var fullPath = Path.Combine(_sourceRoot, fileLocation)
               .Replace('\\', Path.DirectorySeparatorChar)
               .Replace('/', Path.DirectorySeparatorChar);
            if (!File.Exists(fullPath))
                return $"// ERROR: File not found: {fullPath}";

            var lines = File.ReadAllLines(fullPath);

            var tag = $"<!-- Title=\"{title}\" -->";

            bool inside = false;
            var buffer = new List<string>();

            foreach (var line in lines)
            {
                if (line.Contains(tag))
                {
                    if (!inside)
                    {
                        inside = true;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                if (inside)
                    buffer.Add(line);
            }

            if (buffer.Count == 0)
                return $"// ERROR: Region \"{title}\" not found in {fileLocation}";

            return string.Join("\n", buffer);
        }
    }
}
