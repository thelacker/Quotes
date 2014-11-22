using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Котировки
{
    class Download
    {
        private string                        tickerPath;                 /// путь к файлу тиккеров
        private Dictionary<string, string>    generatedLinks;             /// список ссылок для скачивания
        private List<string>                  loadedFiles;                /// список загруженных файлов
        private string                        pathToFolder;               /// путь к папке загрузки

        /// <summary>
        /// Конструктор, создающий менеджер загрузок
        /// </summary>
        /// <param name="path">Путь к файлу с тиккерами</param>
        public Download(string path)
        {
            Form1 prForm1 = new Form1();
            loadedFiles = new List<string>();
            generatedLinks = new Dictionary<string, string>();
            tickerPath = path;
            string[] values = File.ReadAllLines(tickerPath);         /// считываем все строки в файле
            foreach (string token in values)
            {
                string newLink = Link(token, 31, 10, 2014, 'd', 1, 1, 1900);
                generatedLinks.Add(token, newLink);
            }
            foreach(var link in generatedLinks)
            {
                SaveFilesFromYahoo(link.Value, link.Key);
            }

        }

        /// <summary>
        /// Генерация ссылок
        /// </summary>
        /// <param name="name">Имя котировки</param>
        /// <param name="month">Текущий месяц 1-Январь, 2-Февраль и т.д.)</param>
        /// <param name="day">Текущий день</param>
        /// <param name="year">Текущий год</param>
        /// <param name="timeframe">Таймфрейм (d - день)</param>
        /// <param name="lastMoth">Месяц последней записи в файле</param>
        /// <param name="lastDay">День последней записи в файле</param>
        /// <param name="lastYear">Год последней записи в файле</param>
        /// <returns>Возвращает ссылку на скачивание нужного файла</returns>
        public string Link(string name, int month, int day, int year, char timeframe, int lastMoth, int lastDay, int lastYear)
        {
            return ("http://real-chart.finance.yahoo.com/table.csv?s=" + name + "&d=" + month + "&e=" + day + "&f=" + year + "&g=" + timeframe + "&a=" + lastMoth + "&b=" + lastDay + "&c=" + lastYear + "&ignore=.csv");
        }

        /// <summary>
        /// Функция загрузки файлов с сайта Yahoo
        /// </summary>
        /// <param name="link">Сгенерированная ссылка</param>
        public void SaveFilesFromYahoo(string link, string fileName)
        {
            var webClient = new WebClient();
            
            if (pathToFolder == null)
                webClient.DownloadFile(link, fileName);
            else
                webClient.DownloadFile(link, pathToFolder+fileName);
        }

        public void ChangeDestanationFolder(string path)
        {
            pathToFolder = path;
        }

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        public Download()
        {
            loadedFiles     =   new List<string>();
            generatedLinks  =   new Dictionary<string, string>();
        }
    }
}
