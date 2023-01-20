using System;
using System.Collections.Generic;
using System.Globalization;

namespace Localizer
{
	public interface ILocalizationSource
	{
		


		public string GetValue(string key, CultureInfo cultureInfo);
	}
}

