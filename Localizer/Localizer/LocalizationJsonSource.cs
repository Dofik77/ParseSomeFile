using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Localizer
{
	public class LocalizationJsonSource : ILocalizationSource
	{
        public string path;

        public LocalizationJsonSource(string path) => this.path = path;

        public string GetValue(string key, CultureInfo cultureInfo)
        {
            string json = File.ReadAllText(path);

            var str = JsonConvert.DeserializeObject<LocalizationCulture[]>(json)
                .Where(x => x.Name == cultureInfo.Name)
                .Select(o => o.Resource.ToDictionary(z => z.Key, y => y.Value)).FirstOrDefault();

            if (str != null && str.TryGetValue(key,out var result))
            {
                return result;
            }

            return string.Empty;
        }
    }
}

