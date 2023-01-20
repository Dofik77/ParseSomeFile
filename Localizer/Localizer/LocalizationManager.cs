using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;

namespace Localizer
{
    public class LocalizationManager
    {
        private List<ILocalizationSource> sourceList = new List<ILocalizationSource>();


        public string GetString(string key, CultureInfo cultureInfo = null)
        {
            if(cultureInfo == null)
            {
                cultureInfo = CultureInfo.CurrentCulture;
            }

            var result = string.Empty;
            var index = 0;

            while (string.IsNullOrEmpty(result) && index!=sourceList.Count)
            {
                result = sourceList[(index++)%sourceList.Count]?.GetValue(key, cultureInfo);
            }

            return result;
        }

        public void RegisterSource(ILocalizationSource soure) => sourceList.Add(soure);
    }
}

