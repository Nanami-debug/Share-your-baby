using System.Collections.Generic;
using System.IO;

namespace Form.Util
{
    public class IniParser
    {
        private Dictionary<string, Dictionary<string, string>> iniData =
            new Dictionary<string, Dictionary<string, string>>();

        public IniParser(string fileName)
        {
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string filePath = Path.Combine(appPath, fileName);

            if (!File.Exists(filePath)) return;

            string currentSection = "";
            foreach (string line in File.ReadAllLines(filePath))
            {
                string trimmedLine = line.Trim();
                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2);
                    continue;
                }

                if (!string.IsNullOrEmpty(currentSection) && trimmedLine.Contains("="))
                {
                    string[] parts = trimmedLine.Split(new[] { '=' }, 2);
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();
                        if (!iniData.ContainsKey(currentSection))
                        {
                            iniData[currentSection] = new Dictionary<string, string>();
                        }
                        iniData[currentSection][key] = value;
                    }
                }
            }
        }

        public string Read(string section, string key, string defaultValue = "")
        {
            if (iniData.TryGetValue(section, out var sectionData))
            {
                if (sectionData.TryGetValue(key, out var value))
                {
                    return value;
                }
            }
            return defaultValue;
        }
    }
}