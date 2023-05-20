namespace DockFlow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                label1.Text = "��������� ����";
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            label1.Text = "���������� �������� � ������ �������";
            e.Data.GetData(DataFormats.FileDrop);
        }

        private void panel1_DragLeave(object sender, EventArgs e)
        {
            label1.Text = "���������� �������� � ������ �������";
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "�������� ��������";
            file.InitialDirectory = @"%HOMEPATH%";
            file.Filter = "�������� | *.doc*";
            if (file.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}