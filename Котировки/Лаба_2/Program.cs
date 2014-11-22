using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StockQuotes
{
    ///класс работы с котировками
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу с котировками");
            string path = Console.ReadLine();                           // считываем путь к файлу
            Console.WriteLine("Введите начало периода");
            string startDate = Console.ReadLine();
            Console.WriteLine("Введите конец периода");
            string endDate = Console.ReadLine();
            DateTime startD = Convert.ToDateTime(startDate);
            DateTime endD = Convert.ToDateTime(endDate);
            Quotes q = new Quotes(path, new TimeSpan(1, 0, 0, 0));      // новый класс
            Console.WriteLine("Average {0}, Min {1}, Max {2}", q.GetAvr(startD, endD), q.GetMin(startD, endD), q.GetMax(startD, endD));
        }
    }
}
