using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.ComponentModel;

namespace Котировки
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новыйФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изПапкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изЗагруженныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлТиккетовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изПапкиToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.показатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.графикToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заВесьПериодToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заПериодToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.downloadBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.downloadBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(0, 32);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(611, 438);
            this.zedGraphControl1.TabIndex = 0;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // listBox1
            // 
            this.listBox1.AllowDrop = true;
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 31;
            this.listBox1.Location = new System.Drawing.Point(618, 63);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(116, 407);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйФайлToolStripMenuItem,
            this.файлТиккетовToolStripMenuItem});
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(97, 24);
            this.добавитьToolStripMenuItem.Text = "Добавить...";
            // 
            // новыйФайлToolStripMenuItem
            // 
            this.новыйФайлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.изПапкиToolStripMenuItem,
            this.изЗагруженныхToolStripMenuItem});
            this.новыйФайлToolStripMenuItem.Name = "новыйФайлToolStripMenuItem";
            this.новыйФайлToolStripMenuItem.Size = new System.Drawing.Size(200, 24);
            this.новыйФайлToolStripMenuItem.Text = "Файл котировки...";
            this.новыйФайлToolStripMenuItem.Click += new System.EventHandler(this.новыйФайлToolStripMenuItem_Click);
            // 
            // изПапкиToolStripMenuItem
            // 
            this.изПапкиToolStripMenuItem.Name = "изПапкиToolStripMenuItem";
            this.изПапкиToolStripMenuItem.Size = new System.Drawing.Size(201, 24);
            this.изПапкиToolStripMenuItem.Text = "Из папки";
            this.изПапкиToolStripMenuItem.Click += new System.EventHandler(this.изПапкиToolStripMenuItem_Click);
            // 
            // изЗагруженныхToolStripMenuItem
            // 
            this.изЗагруженныхToolStripMenuItem.Enabled = false;
            this.изЗагруженныхToolStripMenuItem.Name = "изЗагруженныхToolStripMenuItem";
            this.изЗагруженныхToolStripMenuItem.Size = new System.Drawing.Size(201, 24);
            this.изЗагруженныхToolStripMenuItem.Text = "Из загруженных...";
            this.изЗагруженныхToolStripMenuItem.Click += new System.EventHandler(this.изЗагруженныхToolStripMenuItem_Click);
            // 
            // файлТиккетовToolStripMenuItem
            // 
            this.файлТиккетовToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.изПапкиToolStripMenuItem1});
            this.файлТиккетовToolStripMenuItem.Name = "файлТиккетовToolStripMenuItem";
            this.файлТиккетовToolStripMenuItem.Size = new System.Drawing.Size(200, 24);
            this.файлТиккетовToolStripMenuItem.Text = "Файл тиккеров";
            // 
            // изПапкиToolStripMenuItem1
            // 
            this.изПапкиToolStripMenuItem1.Name = "изПапкиToolStripMenuItem1";
            this.изПапкиToolStripMenuItem1.Size = new System.Drawing.Size(151, 24);
            this.изПапкиToolStripMenuItem1.Text = "Из папки...";
            this.изПапкиToolStripMenuItem1.Click += new System.EventHandler(this.изПапкиToolStripMenuItem1_Click);
            // 
            // показатьToolStripMenuItem
            // 
            this.показатьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.графикToolStripMenuItem});
            this.показатьToolStripMenuItem.Name = "показатьToolStripMenuItem";
            this.показатьToolStripMenuItem.Size = new System.Drawing.Size(94, 24);
            this.показатьToolStripMenuItem.Text = "Показать...";
            // 
            // графикToolStripMenuItem
            // 
            this.графикToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.заВесьПериодToolStripMenuItem,
            this.заПериодToolStripMenuItem});
            this.графикToolStripMenuItem.Name = "графикToolStripMenuItem";
            this.графикToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.графикToolStripMenuItem.Text = "График";
            // 
            // заВесьПериодToolStripMenuItem
            // 
            this.заВесьПериодToolStripMenuItem.Enabled = false;
            this.заВесьПериодToolStripMenuItem.Name = "заВесьПериодToolStripMenuItem";
            this.заВесьПериодToolStripMenuItem.Size = new System.Drawing.Size(185, 24);
            this.заВесьПериодToolStripMenuItem.Text = "За весь период";
            this.заВесьПериодToolStripMenuItem.Click += new System.EventHandler(this.заВесьПериодToolStripMenuItem_Click);
            // 
            // заПериодToolStripMenuItem
            // 
            this.заПериодToolStripMenuItem.Enabled = false;
            this.заПериодToolStripMenuItem.Name = "заПериодToolStripMenuItem";
            this.заПериодToolStripMenuItem.Size = new System.Drawing.Size(185, 24);
            this.заПериодToolStripMenuItem.Text = "За период...";
            this.заПериодToolStripMenuItem.Click += new System.EventHandler(this.заПериодToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.показатьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1015, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Location = new System.Drawing.Point(740, 63);
            this.listBox2.MultiColumn = true;
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(263, 324);
            this.listBox2.TabIndex = 3;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label1.Location = new System.Drawing.Point(619, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Загруженные";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label2.Location = new System.Drawing.Point(741, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Доступные для загрузки";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(740, 397);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(262, 66);
            this.button1.TabIndex = 6;
            this.button1.Text = "Загрузить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(740, 354);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(262, 37);
            this.progressBar1.TabIndex = 7;
            this.progressBar1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 475);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quotes";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.downloadBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        StockQuotes.Quotes q;
        public DateTime start;
        public int manage = 0;
        public DateTime end;
        static BackgroundWorker bw = new BackgroundWorker();
        private string tickerPath;                 /// путь к файлу тиккеров
        private Dictionary<string, string> generatedLinks;             /// список ссылок для скачивания
        private List<string> loadedFiles;                /// список загруженных файлов
        private string pathToFolder;               /// путь к папке загрузки
        private ZedGraph.ZedGraphControl zedGraphControl1;
        delegate void Del(bool i);
        delegate void Del1(int i);
        delegate void Del2(string i);
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новыйФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изПапкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изЗагруженныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem файлТиккетовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изПапкиToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem показатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem графикToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заВесьПериодToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заПериодToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.BindingSource downloadBindingSource;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

