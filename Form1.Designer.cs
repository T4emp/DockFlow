namespace DockFlow
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            templateToolStripMenuItem = new ToolStripMenuItem();
            addToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            objectToolStripMenuItem = new ToolStripMenuItem();
            createObjectToolStripMenuItem = new ToolStripMenuItem();
            duplicateObjectToolStripMenuItem = new ToolStripMenuItem();
            editObjectNameToolStripMenuItem = new ToolStripMenuItem();
            deleteObjectToolStripMenuItem = new ToolStripMenuItem();
            documentToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            buttonToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            dataGridView1 = new DataGridView();
            tableLayoutPanel2 = new TableLayoutPanel();
            panel1 = new Panel();
            listView2 = new ListView();
            columnHeader2 = new ColumnHeader();
            textBox2 = new TextBox();
            panel2 = new Panel();
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            textBox1 = new TextBox();
            menuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { templateToolStripMenuItem, objectToolStripMenuItem, documentToolStripMenuItem, aboutToolStripMenuItem, buttonToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(910, 24);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // templateToolStripMenuItem
            // 
            templateToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addToolStripMenuItem, exportToolStripMenuItem, deleteToolStripMenuItem });
            templateToolStripMenuItem.Name = "templateToolStripMenuItem";
            templateToolStripMenuItem.Size = new Size(64, 20);
            templateToolStripMenuItem.Text = "Шаблон";
            templateToolStripMenuItem.Click += templateToolStripMenuItem_Click;
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(133, 22);
            addToolStripMenuItem.Text = "Добавить";
            addToolStripMenuItem.Click += addToolStripMenuItem_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(133, 22);
            exportToolStripMenuItem.Text = "Сохранить";
            exportToolStripMenuItem.Click += exportToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(133, 22);
            deleteToolStripMenuItem.Text = "Удалить";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // objectToolStripMenuItem
            // 
            objectToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { createObjectToolStripMenuItem, duplicateObjectToolStripMenuItem, editObjectNameToolStripMenuItem, deleteObjectToolStripMenuItem });
            objectToolStripMenuItem.Name = "objectToolStripMenuItem";
            objectToolStripMenuItem.Size = new Size(59, 20);
            objectToolStripMenuItem.Text = "Объект";
            objectToolStripMenuItem.Click += objectToolStripMenuItem_Click;
            // 
            // createObjectToolStripMenuItem
            // 
            createObjectToolStripMenuItem.Name = "createObjectToolStripMenuItem";
            createObjectToolStripMenuItem.Size = new Size(189, 22);
            createObjectToolStripMenuItem.Text = "Создать";
            createObjectToolStripMenuItem.Click += createObjectToolStripMenuItem_Click;
            // 
            // duplicateObjectToolStripMenuItem
            // 
            duplicateObjectToolStripMenuItem.Name = "duplicateObjectToolStripMenuItem";
            duplicateObjectToolStripMenuItem.Size = new Size(189, 22);
            duplicateObjectToolStripMenuItem.Text = "Создать по аналогии";
            duplicateObjectToolStripMenuItem.Click += duplicateObjectToolStripMenuItem_Click;
            // 
            // editObjectNameToolStripMenuItem
            // 
            editObjectNameToolStripMenuItem.Name = "editObjectNameToolStripMenuItem";
            editObjectNameToolStripMenuItem.Size = new Size(189, 22);
            editObjectNameToolStripMenuItem.Text = "Изменить имя";
            editObjectNameToolStripMenuItem.Click += editObjectNameToolStripMenuItem_Click;
            // 
            // deleteObjectToolStripMenuItem
            // 
            deleteObjectToolStripMenuItem.Name = "deleteObjectToolStripMenuItem";
            deleteObjectToolStripMenuItem.Size = new Size(189, 22);
            deleteObjectToolStripMenuItem.Text = "Удалить";
            deleteObjectToolStripMenuItem.Click += deleteObjectToolStripMenuItem_Click;
            // 
            // documentToolStripMenuItem
            // 
            documentToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveAsToolStripMenuItem });
            documentToolStripMenuItem.Name = "documentToolStripMenuItem";
            documentToolStripMenuItem.Size = new Size(73, 20);
            documentToolStripMenuItem.Text = "Документ";
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(133, 22);
            saveAsToolStripMenuItem.Text = "Сохранить";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { helpToolStripMenuItem });
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(94, 20);
            aboutToolStripMenuItem.Text = "О программе";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(140, 22);
            helpToolStripMenuItem.Text = "Инструкция";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // buttonToolStripMenuItem
            // 
            buttonToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            buttonToolStripMenuItem.Name = "buttonToolStripMenuItem";
            buttonToolStripMenuItem.Size = new Size(117, 20);
            buttonToolStripMenuItem.Text = "Изменить данные";
            buttonToolStripMenuItem.Click += buttonToolStripMenuItem_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel1.Controls.Add(dataGridView1, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 24);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(910, 548);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(367, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(540, 542);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.RowsRemoved += dataGridView1_RowsRemoved;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Controls.Add(panel1, 0, 1);
            tableLayoutPanel2.Controls.Add(panel2, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(358, 542);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Controls.Add(listView2);
            panel1.Controls.Add(textBox2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 274);
            panel1.Name = "panel1";
            panel1.Size = new Size(352, 265);
            panel1.TabIndex = 3;
            // 
            // listView2
            // 
            listView2.Columns.AddRange(new ColumnHeader[] { columnHeader2 });
            listView2.Dock = DockStyle.Fill;
            listView2.Location = new Point(0, 23);
            listView2.MultiSelect = false;
            listView2.Name = "listView2";
            listView2.Size = new Size(352, 242);
            listView2.TabIndex = 1;
            listView2.UseCompatibleStateImageBehavior = false;
            listView2.View = View.Details;
            listView2.SelectedIndexChanged += listView2_SelectedIndexChanged;
            listView2.KeyDown += listView2_KeyDown;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Объекты";
            // 
            // textBox2
            // 
            textBox2.Dock = DockStyle.Top;
            textBox2.ForeColor = Color.Silver;
            textBox2.Location = new Point(0, 0);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(352, 23);
            textBox2.TabIndex = 0;
            textBox2.Text = "Поиск";
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // panel2
            // 
            panel2.Controls.Add(listView1);
            panel2.Controls.Add(textBox1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(352, 265);
            panel2.TabIndex = 4;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
            listView1.Dock = DockStyle.Fill;
            listView1.Location = new Point(0, 23);
            listView1.MultiSelect = false;
            listView1.Name = "listView1";
            listView1.Size = new Size(352, 242);
            listView1.TabIndex = 3;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            listView1.KeyDown += listView1_KeyDown;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Шаблоны";
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Top;
            textBox1.ForeColor = Color.Silver;
            textBox1.Location = new Point(0, 0);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(352, 23);
            textBox1.TabIndex = 2;
            textBox1.Text = "Поиск";
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(910, 572);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(600, 400);
            Name = "Form1";
            Text = "DockFlow";
            Load += Form1_Load;
            SizeChanged += Form1_SizeChanged;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem templateToolStripMenuItem;
        private ToolStripMenuItem objectToolStripMenuItem;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem createObjectToolStripMenuItem;
        private ToolStripMenuItem editObjectNameToolStripMenuItem;
        private ToolStripMenuItem deleteObjectToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel1;
        private ToolStripMenuItem documentToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox textBox2;
        private Panel panel1;
        private Panel panel2;
        private ListView listView2;
        private ListView listView1;
        private TextBox textBox1;
        private DataGridView dataGridView1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader1;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem buttonToolStripMenuItem;
        private ToolStripMenuItem duplicateObjectToolStripMenuItem;
    }
}