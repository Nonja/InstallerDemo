using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for SetupView.xaml
    /// </summary>
    public partial class SetupView : Window, ISetupView
    {
        public SetupView()
        {
            InitializeComponent();
            SetupPresenter pr = new SetupPresenter(this);
            pr.sourceInfo();

        }

        //FolderBrowserDialog for choosing or creating destination folder
        private void button1_Click(object sender, EventArgs e)
        {
            string folderPath = "";
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
                textBox.Text = folderPath;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            SetupPresenter pr = new SetupPresenter(this);
            pr.StartInstall();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

       
        public void Message(string msg)
        {
            MessageBox.Show(msg);
        }


        public string destinationDirectory
        {
            get {   return textBox.Text;   }
            set {   textBox.Text = value;  }
        }


        public string numberOfFiles
        {
            get { return block1.Text; }
            set { block1.Text = value; }
        }


        public string progressOfFiles
        {
            get { return block2.Text; }
            set { block2.Text = value; }
        }

        public string msg { get; set; }


        public void InstallDone()
        {
            if (MessageBox.Show("Installing Done!") == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
                return;
            }
            
        }
    }
}
