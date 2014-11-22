// Директивы
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
using System.Xml.Serialization;

namespace Котировки
{
    public partial class Form1 : Form
    {
        // Работа с формой
        public Form1()  
        {
            InitializeComponent();
            fileinfos = new Dictionary<string, string>();
        }                                                                                                                           // Загрузка формы
        private void zedGraphControl1_Load                  (object sender, EventArgs e)
        {

        }                                                           // Загрузка ZedGraph
        private void Form1_Load                             (object sender, EventArgs e)
        {

        }                                                           // Загрузка предыдущего сеанса
        private void Form1_Activated                        (object sender, EventArgs e)
        {
            try
            {
                // Обеъект десериализации
                formatter = new XmlSerializer(typeof(Ser));

                // Десериализируем
                using (FileStream fs = new FileStream("quotes.xml", FileMode.OpenOrCreate))
                {
                    Ser newSerialize = (Ser)formatter.Deserialize(fs);

                    // Достаем поля
                    start       =   newSerialize.start;
                    end         =   newSerialize.end;
                    pathtofile  =   newSerialize.quote;

                    // Достаем период
                    toolStripComboBox1.SelectedItem = Convert.ToString(newSerialize.period);
                    
                    // Новый объект котировок
                    q           =   new StockQuotes.Quotes(pathtofile, new TimeSpan(1, 0, 0, 0));
                    FileInfo fi =   new FileInfo(pathtofile);

                    // Активаци рисовки
                    listBox1.Items.Add(fi.FullName);
                    заВесьПериодToolStripMenuItem.Enabled    = true;
                    заПериодToolStripMenuItem.Enabled        = true;
                    ценовойКаналToolStripMenuItem.Enabled    = true;
                }
            }

            // В случае отсутствия объекта сериализации
            catch (Exception ex)
            {
                start       = Convert.ToDateTime(0);
                end         = Convert.ToDateTime(0);
                pathtofile  = null;

                toolStripComboBox1.SelectedItem = "1";
            }
        }                                                           // Активация формы и десериализация 
        private void выходToolStripMenuItem_Click           (object sender, EventArgs e)
        {
            
        }                                                           // Выход из формы
        private void Form1_FormClosing                      (object sender, FormClosingEventArgs e)
        {
            // Сериализация
            if (start != null)
                Serialize = new Ser(pathtofile, start, end, Convert.ToInt32(toolStripComboBox1.SelectedItem));
            else
                Serialize = new Ser(pathtofile, Convert.ToDateTime(0), Convert.ToDateTime(0), Convert.ToInt32(toolStripComboBox1.SelectedItem));

            // Объект сериализации
            formatter = new XmlSerializer(typeof(Ser));

            // Сериализация в файл
            using (FileStream fs = new FileStream("quotes.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Serialize);
            }
        }                                                // Закрытие формы и сериализация
        private void menuStrip1_ItemClicked                 (object sender, ToolStripItemClickedEventArgs e)
        {

        }                                       // Активация верхнего меню


        // Верхнее меню
        private void изПапкиToolStripMenuItem_Click         (object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();      // Показывает дислоговое окно с выором файла
            openFileDialog1.Filter = "Quotes Files|*.csv";
            openFileDialog1.Title = "Select a quotes File";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)                                // открывает выделенный файл
            {
                pathtofile = openFileDialog1.FileName;
                q = new StockQuotes.Quotes(openFileDialog1.FileName, new TimeSpan(1, 0, 0, 0));                      // Создаем новый класс котировок
            }
            FileInfo fi = new FileInfo(openFileDialog1.FileName);
            listBox1.Items.Add(fi.FullName);
            заВесьПериодToolStripMenuItem.Enabled = true;
            заПериодToolStripMenuItem.Enabled = true;
            ценовойКаналToolStripMenuItem.Enabled = true;
        }  // Котировка из папки
        private void заВесьПериодToolStripMenuItem_Click    (object sender, EventArgs e)
        {
            ShowCandles();
        }  // Зарисовка графика за весь период
        private void заПериодToolStripMenuItem_Click        (object sender, EventArgs e)
        {
            if (zedGraphControl1.GraphPane.CurveList.Count > 0)                                                                     // Удаляет прошлый график
                zedGraphControl1.GraphPane.CurveList.RemoveAt(0);

            Form f = new Form2();
            f.Owner = this;
            f.Show();

        }  // Выставление периода
        private void новыйФайлToolStripMenuItem_Click       (object sender, EventArgs e)
        {

        }  // Тиккеры
        private void изЗагруженныхToolStripMenuItem_Click   (object sender, EventArgs e)
        {

        }  // Тиккеры из загруженных
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

        }  // Тиккеры из папки
        private void показатьToolStripMenuItem_Click        (object sender, EventArgs e)
        {

        }  // Показ графиков
        private void ценовойКаналToolStripMenuItem_Click    (object sender, EventArgs e)
        {
            ShowPriceChannel();
        }  // Показ ценового канала

        // Работа с загрузкой
        /// <summary>
        /// Конструктор, создающий менеджер загрузок
        /// </summary>
        /// <param name="path">Путь к файлу с тиккерами</param>
        public void Download                (string path)
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
            foreach (var link in generatedLinks)
            {
                SaveFilesFromYahoo(link.Value, link.Key);
                progressBar1.Invoke(new Del1((i) => progressBar1.Value = i), p++);
                string newpath = Directory.GetCurrentDirectory();
                string newstring = (newpath + "\\" + link.Key + ".csv");
                listBox1.Invoke(new Del2((s) => listBox1.Items.Add(s)), newstring);
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
        public string Link                  (string name, int month, int day, int year, char timeframe, int lastMoth, int lastDay, int lastYear)
        {
            return ("http://real-chart.finance.yahoo.com/table.csv?s=" + name + "&d=" + month + "&e=" + day + "&f=" + year + "&g=" + timeframe + "&a=" + lastMoth + "&b=" + lastDay + "&c=" + lastYear + "&ignore=.csv");
        }
        /// <summary>
        /// Функция загрузки файлов с сайта Yahoo
        /// </summary>
        /// <param name="link">Сгенерированная ссылка</param>
        public void SaveFilesFromYahoo      (string link, string fileName)
        {
            var webClient = new WebClient();

            if (pathToFolder == null)
                webClient.DownloadFile(link, fileName + ".csv");
            else
                webClient.DownloadFile(link, pathToFolder + fileName + ".csv");
        }
        /// <summary>
        /// Изменение пути назначения загрузки
        /// </summary>
        /// <param name="path">Путь к папке загрузки</param>
        public void ChangeDestanationFolder (string path)
        {
            pathToFolder = path;
        }
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        public void Download()                           
        {
            loadedFiles = new List<string>();
            generatedLinks = new Dictionary<string, string>();
        }

        // Элементы формы
        // Работа с листбоксом
        private void listBox1_SelectedIndexChanged  (object sender, EventArgs e)
        {

        }
        // Кнопка
        private void button1_Click                  (object sender, EventArgs e)
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
        private void progressBar1_Click             (object sender, EventArgs e)
        {

        }
        private void listBox2_SelectedIndexChanged  (object sender, EventArgs e)
        {
        }
        private void listBox1_MouseDoubleClick      (object sender, MouseEventArgs e)
        {
            q = new StockQuotes.Quotes(Convert.ToString(listBox1.SelectedItem), new TimeSpan(1, 0, 0, 0));
            ShowCandles();
            ShowPriceLine();
            ShowPriceChannel();
        }

        // Работа с графиками
        /// <summary>
        /// Показ свечей
        /// </summary>
        public void ShowCandles()
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.YAxis.MajorGrid.IsVisible = true;
            pane.XAxis.MajorGrid.IsVisible = true;
            // Set the title and axis labels   
            //pane.Title.Text = "Japanese Candlestick Chart Demo";
            //pane.XAxis.Title.Text = "Trading Date";
            //pane.YAxis.Title.Text = "Share Price, $US";                                                                          

            // Создадим две панели для графика, где будут отображаться
            // одинаковые данные, но с разными значениями BarType
            // Количество столбцов
            int itemscount = q.quotesList.Capacity;
            int bars = 0;
            StockPointList spl = new StockPointList();
            foreach(var bar in q.quotesList)
            {
                DateTime x = bar.starttime;
                double close = bar.closePrice;
                double hi = bar.maxPrice;
                double open = bar.openPrice;
                double low = bar.minPrice;

                StockPt pt = new StockPt(new XDate(x), hi, low, open, close, 100000);
                spl.Add(pt);

                //open = close;
                // Advance one day
                //xDate.AddDays(1.0);
                // but skip the weekends
                //if (XDate.XLDateToDayOfWeek(xDate.XLDate) == 6)
                // xDate.AddDays(2.0);
            }

            JapaneseCandleStickItem myCurve = pane.AddJapaneseCandleStick("trades", spl);
            myCurve.Stick.IsAutoSize = true;
            //myCurve.Stick.Color = Color.Blue;

            // Use DateAsOrdinal to skip weekend gaps
            pane.XAxis.Type = AxisType.DateAsOrdinal;
            //myPane.XAxis.Scale.Min = new XDate(2006, 1, 1);

            // pretty it up a little
            //pane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0f);
            //pane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45.0f);

            // Tell ZedGraph to calculate the axis ranges
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        /// <summary>
        /// Показывает графиг скользящей средней
        /// </summary>
        public void ShowPriceLine()
        {
            // Создадим список точек
            GraphPane pane = zedGraphControl1.GraphPane;
            PointPairList list = new PointPairList();
            int i = 0;
            start = q.quotesList.Min(x => x.starttime);
            end = q.quotesList.Max(x => x.starttime);
            q.GetPerAvr(start, end);
            foreach (var quote in q.averDict)
            {
                double y = quote.Value;
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
        /// <summary>
        /// Показы графика ценового канала
        /// </summary>
        public void ShowPriceChannel()
        {
            // Создадим список точек
            GraphPane pane = zedGraphControl1.GraphPane;
            PointPairList list = new PointPairList();
            PointPairList list2 = new PointPairList();
            int i = 0;
            if (toolStripComboBox1.SelectedItem == null)
            {
                toolStripComboBox1.SelectedItem = "1";
                MessageBox.Show("Определите время ценового канала");
            }
            int period = Convert.ToInt32(toolStripComboBox1.SelectedItem);
            if (start == null)
            {
                start = q.quotesList.Min(x => x.starttime);
                end = q.quotesList.Max(x => x.starttime);
            }
            q.PriceChannel(Convert.ToInt32(toolStripComboBox1.SelectedItem));
            foreach (var quote in q.ChannelMax)
            {
                for (int j = 0; j < period; j++)
                {
                    double y = quote;
                    double x = i++;
                    list.Add(x, y);
                }
            }
            i = 0;
            foreach (var quote in q.ChannelMin)
            {
                for (int j = 0; j < period; j++)
                {
                    double y = quote;
                    double x = i++;
                    list2.Add(x, y);
                }
            }
            // Работаем с линией
            Color curveColor = Color.Green;                                                              // Цвет линии
            LineItem myCurve = pane.AddCurve("", list, curveColor, SymbolType.None);                   // Создание линии
            curveColor = Color.Yellow;                                                                 // Цвет линии
            LineItem myCurve2 = pane.AddCurve("", list2, curveColor, SymbolType.None);                 // Создание линии
            myCurve.Line.IsSmooth = true;                                                              // Включим сглаживание
            zedGraphControl1.AxisChange();                                                             // Обновим график
            zedGraphControl1.Invalidate();
        }
        
        // Поток
        public void bwDownload                              (object sender, DoWorkEventArgs e)
        {
            Download(Convert.ToString(e.Argument));
        }
    }
}
