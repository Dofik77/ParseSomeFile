using System;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace Localizer.LocalizationModel
{
	public class LocalizationXMLSource : ILocalizationSource
    {
        public string path;

        public LocalizationXMLSource(string path) => this.path = path;


        public string GetValue(string key, CultureInfo cultureInfo)
        {
            string file = File.ReadAllText(path);
            XmlSerializer serializer = new XmlSerializer(typeof(LocalizationCulture[]));

            using(FileStream fs = new FileStream("somefile.xml", FileMode.OpenOrCreate))
            {
                LocalizationCulture[] resourceCulture =
                    serializer.Deserialize(fs) as LocalizationCulture[];

                var str = resourceCulture.Where(x => x.Name == cultureInfo.Name).
                    Select(o => o.Resource.ToDictionary(z => z.Key, y => y.Value)).FirstOrDefault();

               if(str != null && str.TryGetValue(key, out var result))
               {
                    return result;
               }

               return string.Empty;
            }
        }
    }
}

