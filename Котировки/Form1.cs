using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ZedGraph;
using System.Net;
using System.Threading;

namespace Котировки
{
    public partial class Form1 : Form
    {
        
        /// <summary>
        /// Создание формы
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        private void zedGraphControl1_Load                  (object sender, EventArgs e)
        {

        }
        private void Form1_Load                             (object sender, EventArgs e)
        {

        }
        private void menuStrip1_ItemClicked                 (object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void выходToolStripMenuItem_Click           (object sender, EventArgs e)
        {
            
        }

        // Верхнее меню
        private void изПапкиToolStripMenuItem_Click         (object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();      // Показывает дислоговое окно с выором файла
            openFileDialog1.Filter = "Quotes Files|*.csv";
            openFileDialog1.Title = "Select a quotes File";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)                                // открывает выделенный файл
            {
                q = new StockQuotes.Quotes(openFileDialog1.FileName, new TimeSpan(1, 0, 0, 0));                      // Создаем новый класс котировок
            }
            FileInfo fi = new FileInfo(openFileDialog1.FileName);
            listBox1.Items.Add(fi.Name.Replace(".csv", ""));
            заВесьПериодToolStripMenuItem.Enabled = true;
            заПериодToolStripMenuItem.Enabled = true;
        }
        private void заВесьПериодToolStripMenuItem_Click    (object sender, EventArgs e)
        {
            // Удаляем прошлый график
            if (zedGraphControl1.GraphPane.CurveList.Count > 0)     
                zedGraphControl1.GraphPane.CurveList.RemoveAt(0);

            // Создадим список точек
            GraphPane pane      =   zedGraphControl1.GraphPane;  
            PointPairList list  =   new PointPairList();                                                       
            int i = 0;
            foreach (var quote in q.quotesList)
            {
                double y = quote.closePrice;
                double x = i++;
                list.Add(x, y);
            }

            // Работаем с линией
            Color curveColor = Color.Red;                                                              // Цвет линии
            LineItem myCurve = pane.AddCurve("", list, curveColor, SymbolType.None);                   // Создание линии
            myCurve.Line.IsSmooth = true;                                                              // Включим сглаживание
            zedGraphControl1.AxisChange();                                                             // Обновим график
            zedGraphControl1.Invalidate();
        }
        private void заПериодToolStripMenuItem_Click        (object sender, EventArgs e)
        {
            if (zedGraphControl1.GraphPane.CurveList.Count > 0)                                                                     // Удаляет прошлый график
                zedGraphControl1.GraphPane.CurveList.RemoveAt(0);

            Form f = new Form2();
            f.Owner = this;
            f.Show();

        }
        private void новыйФайлToolStripMenuItem_Click       (object sender, EventArgs e)
        {

        }
        private void изЗагруженныхToolStripMenuItem_Click   (object sender, EventArgs e)
        {

        }
        // Работа с тикерами
        private void изПапкиToolStripMenuItem1_Click        (object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();                                                   // Показывает дислоговое окно с выором файла
            openFileDialog1.Filter = "Quotes Files|*.txt";
            openFileDialog1.Title = "Select a quotes File";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)                                // открывает выделенный файл
            {
                tickerPath = openFileDialog1.FileName;
                string[] tickers = File.ReadAllLines(openFileDialog1.FileName);
                foreach (string tick in tickers)
                {
                    listBox2.Items.Add(tick);
                }
            }

        }

        /// <summary>
        /// Функция показа графика
        /// </summary>
        public void ShowGraph()
        {
            /*
            GraphPane pane = zedGraphControl1.GraphPane;                                                                            // 
            PointPairList list = new PointPairList();                                                                               // Создадим список точек

            int i = 0;
            foreach (var quote in q.quotesList.Where(x => x.starttime >= start && x.starttime <= end))
            {
                double y = quote.closePrice;
                double x = i++;
                list.Add(x, y);
            }

            Color curveColor = Color.Red;                                                                                           // Цвет линии
            LineItem myCurve = pane.AddCurve("", list, curveColor, SymbolType.None);                                                // Создание линии
            myCurve.Line.IsSmooth = true;                                                                                           // Включим сглаживание

            zedGraphControl1.AxisChange();                                                                                          // Обновим график
            zedGraphControl1.Invalidate();
             */
            // Удалим существующую панель с графиком
            zedGraphControl1.MasterPane.PaneList.Clear();

            // Создадим две панели для графика, где будут отображаться
            // одинаковые данные, но с разными значениями BarType
            GraphPane pane = new GraphPane();
            // Количество столбцов
            int itemscount = q.quotesList.Capacity;
            int bars = 0;
            // Сгенерируем данные для высот столбцов
            double[] YValues1 = GenerateData(itemscount, 2, bars);
            double[] YValues2 = GenerateData(itemscount, 1, bars);
            double[] YValues3 = GenerateData(itemscount, 3, bars);
            /*
            pane.XAxis.Type = AxisType.Date;
            pane.XAxis.Scale.Min = new XDate(q.quotesList.Min(x => x.starttime));
            pane.XAxis.Scale.Max = new XDate(q.quotesList.Max(x => x.starttime));
             */
            double[] XValues = new double[itemscount];

            // Заполним данные
            for (int i = 0; i < itemscount; i++)
            {
                XValues[i] = i + 1;
            }

            // По одинаковым данным построим две гистограммы
            CreateBars(pane, XValues, YValues1, YValues2, YValues3);

            // !!! У первого графика столбцы накладываются один на другой
            // всегда в одинаковой последовательности:
            // впереди синий, затем красный, затем желтый
            pane.BarSettings.Type = BarType.Overlay;
            pane.Title.Text = "BarType.Overlay";

            // Добавим созданные панели в MasterPane

            zedGraphControl1.MasterPane.Add(pane);

            // Зададим расположение графиков
            using (Graphics g = CreateGraphics())
            {
                // Графики будут размещены в один столбец друг под другом
                zedGraphControl1.MasterPane.SetLayout(g, PaneLayout.SingleColumn);
            }

            pane.YAxis.Scale.MinAuto = true;
            pane.YAxis.Scale.MaxAuto = true;
            pane.IsBoundedRanges = true;
            // Обновим данные об осях
            zedGraphControl1.AxisChange();

            // Обновляем график
            zedGraphControl1.Invalidate();
        }
        /// <summary>
        /// Сгенерировать данные для графика
        /// </summary>
        /// <param name="itemscount"></param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        private double[] GenerateData                       (int itemscount, int a, int bars)
        {
            double[] values = new double[itemscount];
            int i = 0;
            bars = 0;
            switch (a)
            {
                case 1:
                    i = 0;
                    foreach (var quote in q.quotesList.Where(x => x.starttime >= start && x.starttime <= end))
                    {
                        if (quote.closePrice < quote.openPrice)
                        {
                            values[i++] = quote.openPrice;
                            bars++;
                        }
                        else
                        {
                            values[i++] = 0;
                            bars++;
                        }
                    }
                    break;
                case 2:
                    i = 0;
                    foreach (var quote in q.quotesList.Where(x => x.starttime >= start && x.starttime <= end))
                    {
                        if (quote.closePrice < quote.openPrice)
                        {
                            values[i++] = quote.closePrice;
                            bars++;
                        }
                        else
                        {
                            values[i++] = quote.openPrice;
                            bars++;
                        }
                    }
                    break;
                case 3:
                    i = 0;
                    foreach (var quote in q.quotesList.Where(x => x.starttime >= start && x.starttime <= end))
                    {
                        if (quote.closePrice >= quote.openPrice)
                        {
                            values[i++] = quote.closePrice;
                            bars++;
                        }
                        else
                        {
                            values[i++] = 0;
                            bars++;
                        }
                    }
                    break;
            }

            return values;
        }
        /// <summary>
        /// Создать столбики по данным
        /// </summary>
        /// <param name="pane">Панель, куда добавляются столбцы</param>
        /// <param name="XValues">Координаты по оси X</param>
        /// <param name="YValues1">Данные по оси Y для первого набора столбцов</param>
        /// <param name="YValues2">Данные по оси Y для второго набора столбцов</param>
        /// <param name="YValues3">Данные по оси Y для третьего набора столбцов</param>
        private static void CreateBars(GraphPane pane,
            double[] XValues,
            double[] YValues1, double[] YValues2, double[] YValues3)
        {
            pane.CurveList.Clear();

            // Создадим три гистограммы
            pane.AddBar("", XValues, YValues1, Color.White);
            pane.AddBar("", XValues, YValues2, Color.Red);
            pane.AddBar("", XValues, YValues3, Color.Green);
        }
        
        // Работа с листбоксом
        private void listBox1_SelectedIndexChanged          (object sender, EventArgs e)
        {

        }
        // Кнопка
        private void button1_Click                          (object sender, EventArgs e)
        {
            if (tickerPath != null)
            {
                if (manage == 0)
                {
                    bw.DoWork += bwDownload;
                    bw.RunWorkerAsync(tickerPath);
                    manage++;
                }
            }
            else
                MessageBox.Show("Нет файла тиккеров");
        }
        // ПрогресБар
        private void progressBar1_Click                     (object sender, EventArgs e)
        {

        }
        // Поток
        public void bwDownload                              (object sender, DoWorkEventArgs e)
        {
            Download(Convert.ToString(e.Argument));
        }



        // Работа с загрузкой
        /// <summary>
        /// Конструктор, создающий менеджер загрузок
        /// </summary>
        /// <param name="path">Путь к файлу с тиккерами</param>
        public void Download                    (string path)
        {
            progressBar1.Invoke(new Del((i) => progressBar1.Visible = i), true);
            loadedFiles = new List<string>();
            generatedLinks = new Dictionary<string, string>();
            tickerPath = path;
            string[] values = File.ReadAllLines(tickerPath);         /// считываем все строки в файле
            int p = 0;
            foreach (string token in values)
            {
                string newLink = Link(token, 31, 10, 2014, 'd', 1, 1, 1900);
                generatedLinks.Add(token, newLink);
                progressBar1.Invoke(new Del1((i) => progressBar1.Maximum = i), p++);
            }
            p = 0;
            foreach(var link in generatedLinks)
            {
                SaveFilesFromYahoo(link.Value, link.Key);
                progressBar1.Invoke(new Del1((i) => progressBar1.Value = i), p++);
                listBox1.Invoke(new Del2((s) => listBox1.Items.Add(s)), link.Key);
                listBox2.Invoke(new Del2((s) => listBox2.Items.Remove(s)), link.Key);
            }
            MessageBox.Show("Загрузка завершена");

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
        public string Link                      (string name, int month, int day, int year, char timeframe, int lastMoth, int lastDay, int lastYear)
        {
            return ("http://real-chart.finance.yahoo.com/table.csv?s=" + name + "&d=" + month + "&e=" + day + "&f=" + year + "&g=" + timeframe + "&a=" + lastMoth + "&b=" + lastDay + "&c=" + lastYear + "&ignore=.csv");
        }
        /// <summary>
        /// Функция загрузки файлов с сайта Yahoo
        /// </summary>
        /// <param name="link">Сгенерированная ссылка</param>
        public void SaveFilesFromYahoo          (string link, string fileName)
        {
            var webClient = new WebClient();
            
            if (pathToFolder == null)
                webClient.DownloadFile(link, fileName);
            else
                webClient.DownloadFile(link, pathToFolder+fileName);
        }
        /// <summary>
        /// Изменение пути назначения загрузки
        /// </summary>
        /// <param name="path">Путь к папке загрузки</param>
        public void ChangeDestanationFolder     (string path)
        {
            pathToFolder = path;
        }
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        public void Download()
        {
            loadedFiles     =   new List<string>();
            generatedLinks  =   new Dictionary<string, string>();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
