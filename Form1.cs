using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.IO;
using System.Windows.Forms;

namespace DockFlow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var culture = System.Globalization.CultureInfo.CurrentUICulture;
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                label1.Text = "Отпустите мышь";
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            label1.Text = "Перетащите документ в данную область";
            e.Data.GetData(DataFormats.FileDrop);
        }

        private void panel1_DragLeave(object sender, EventArgs e)
        {
            label1.Text = "Перетащите документ в данную область";
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Выберите документ";
            file.InitialDirectory = @"%HOMEPATH%";
            file.Filter = "Документ | *.doc*";
            if (file.ShowDialog() == DialogResult.OK)
            {
                byte[] readText = File.ReadAllBytes(file.FileName);
                sendToDB(file.SafeFileName, readText);
            }
        }

        public void checkFile(string fileName)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                DocumentTemplate doc = new DocumentTemplate();
                var obj = doc.Name.ToList();
            }
        }

        public void sendToDB(string Name, byte[] File)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                DocumentTemplate doc = new DocumentTemplate { Name = $"{Name}", File = File };
                db.DocumentTemplate.Add(doc);
                db.SaveChanges();
            }
        }
    }
}