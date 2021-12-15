using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using WeightliftingManagment.Localization.LocalizationModel;

namespace WeightliftingManagment.Localization.Readers
{
    public class CsvFileReader
    {
        private readonly string _pathToFile;
        private readonly string _fileName;
        private readonly char _separator;

        public CsvFileReader(string pathToFile, string fileName = "AppLocalization.csv", char separator = ',')
        {
            _pathToFile = pathToFile;
            _fileName = fileName;
            _separator = separator;
        }

        public Dictionary<CultureInfo, Dictionary<string, LocalizationEntry>> GetEntries()
        {

            var listOfLine = ReadFile(_pathToFile, _fileName, _separator);

            return CreateLocalizationEntries(listOfLine);
        }

        private static Dictionary<CultureInfo, Dictionary<string, LocalizationEntry>> CreateLocalizationEntries(List<List<string>> listOfLine)
        {
            var result = new Dictionary<CultureInfo, Dictionary<string, LocalizationEntry>>();

            var langList = listOfLine[0];
            langList.RemoveAt(0);
            listOfLine.RemoveAt(0);

            for (var i = 0; i < langList.Count; i++)
            {
                var langDic = new Dictionary<string, LocalizationEntry>();
                for (var j = 0; j < listOfLine.Count; j++)
                {
                    langDic.Add(listOfLine[j][0], new LocalizationEntry(listOfLine[j][i + 1]));
                }
                result.Add(CultureInfo.GetCultureInfo(langList[i]), langDic);
            }
            return result;
        }

        private static List<List<string>> ReadFile(string pathToFile, string fileName, char separator)
        {
            var listOfListOfstring = new List<List<string>>();
            var listOfLine = File.ReadAllLines(Path.Combine(pathToFile, fileName)).ToList();
            listOfLine.ForEach(line => listOfListOfstring.Add(line.Split(separator).ToList()));

            return listOfListOfstring;
        }
    }
}
