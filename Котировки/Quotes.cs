using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StockQuotes
{
    class Quotes
    {
        public List<Bar> quotesList;                       // массив котировок
        public Dictionary<DateTime, double> averDict;      // Словарь скользящих средних
        public List<double> ChannelMax;
        public List<double> ChannelMin;

        // конструктор класса
        public Quotes(string path, TimeSpan ts)
        {
            string[] values =   File.ReadAllLines(path);             // считываем все строки в файле
            quotesList      =   new List<Bar>();                     // создаем массив котировок
            averDict        =   new Dictionary<DateTime, double>();  // создаем словарь скользящих средних

            // разбиваем каждую строку из массива строк на слова
            foreach (string str in values.Skip(1))
            {
                string [] tokens = str.Split(new Char[] { ',' });
                try
                {
                    DateTime st = Convert.ToDateTime(tokens[0]);                    // строку в время
                    double oP = Convert.ToDouble(tokens[1].Replace(".", ","));      // строку в цену открытия
                    double maxP = Convert.ToDouble(tokens[2].Replace(".", ","));    // строку в цену максимума
                    double minP = Convert.ToDouble(tokens[3].Replace(".", ","));    // строку в цену минимума
                    double cP = Convert.ToDouble(tokens[4].Replace(".", ","));      // строку в цену закрытия


                    Bar b = new Bar(oP, cP, maxP, minP, ts, st);         // новый объект класса Bar по считанным данным
                    quotesList.Add(b);                                   // добавление элемента в список
                }
                catch (Exception e)                                      // Перехватываем исключения
                {
                    Console.WriteLine(e);                                // Выводим исключения на экран
                    continue;
                }
            }
        }

        // получение средней цены закрытия свечи за период дней
        public double GetAvr(DateTime start, DateTime end)
        {
            return quotesList.Where(x => (x.starttime >= start && x.starttime <= end)).Average(b => b.closePrice);
        }

        // получение минимального значение за период
        public double GetMin(DateTime start, DateTime end)
        {
            return quotesList.Where(x => (x.starttime >= start && x.starttime <= end)).Min(b => b.minPrice);
        }

        // получение максимального значения за период
        public double GetMax(DateTime start, DateTime end)
        {
            return quotesList.Where(x => (x.starttime >= start && x.starttime <= end)).Max(b => b.maxPrice);
        }

        // расчет скользящей седней
        public void GetPerAvr(DateTime start, DateTime end)
        {
            foreach (var quote in quotesList.Where(x => (x.starttime >= start && x.starttime <= end)))
            {
                double average = (quote.maxPrice + quote.minPrice) / 2;     // расчет скользящей средней
                averDict.Add(quote.starttime, average);                     // добавление скользящей средней в словарь
            }
        }

        public void PriceChannel(int period)
        {
            ChannelMax = new List<double>();
            ChannelMin = new List<double>();
            for (int K = 0; K < ((quotesList.Count)-(period)); K += period)
            {
                ChannelMax.Add(quotesList.Skip(K).Take(period).Max(x => x.maxPrice));
                ChannelMin.Add(quotesList.Skip(K).Take(period).Min(x => x.minPrice));
            }
        }
        // преобразование таймфрейма
        public void TimeFrameChange(DateTime start, DateTime end)
        {
            Bar b = quotesList.Find(x => x.starttime == start);                                                  // находим начальный объект и присваиваем его значения новому объекту

            b.timeslot      =     new TimeSpan(Convert.ToInt32(start.Date - end.Date), 0, 0, 0);                        // расчитываем временной интервал 
            b.closePrice    =     ((quotesList.Find(x => x.starttime == end)).closePrice);                              // цена закрытия
            b.maxPrice      =     GetMax(start, end);                                                                   // максимальная цена
            b.minPrice      =     GetMin(start, end);                                                                   // минимальная цена

            quotesList.Add(b);                                                                                          // добавление нового элемента в список
        }
    }
}
