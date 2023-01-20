using System;
using System.Globalization;
using System.Linq;
using Localizer;
using Localizer.LocalizationModel;

namespace Initializer
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] words = { "Json", "XML" };

            Console.WriteLine("Введите номер типа файла локализации");

            int index = 0;
            foreach (var word in words.Select(x => x))
            {
                index++;
                Console.Write(index.ToString() + " : ");
                Console.WriteLine(word);
            }

            int fileTypeNumber = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите язык в формате культуры потока 'us-EN', 'ru-Ru' ");
            string localizationCulture = Console.ReadLine();

            Console.WriteLine("Введите ключ-значение для локализации");
            string key = Console.ReadLine();

            InitializeLocalizationManager(localizationCulture, key, fileTypeNumber);
        }


        private static void InitializeLocalizationManager(string localizationCulture, string key, int fileTypeNumber)
        {
            string filePath;

            LocalizationManager localizationManager = new LocalizationManager();

            switch (fileTypeNumber)
            {
                case 1:
                    Console.WriteLine("Ввидте путь к файлу Json");
                    filePath = Console.ReadLine();
                    LocalizationJsonSource localizationJson = new LocalizationJsonSource(filePath);
                    localizationManager.RegisterSource(localizationJson);
                    break;
                case 2:
                    Console.WriteLine("Введите путь к файлу XML");
                    filePath = Console.ReadLine();
                    LocalizationXMLSource localizationXML = new LocalizationXMLSource(filePath);
                    localizationManager.RegisterSource(localizationXML);
                    break;
                default:
                    Console.WriteLine("Этого типа данных не существует");
                    break;
            }

            CultureInfo cultureInfo = new CultureInfo(localizationCulture);
            localizationManager.GetString(key, cultureInfo);
        }
    }
}