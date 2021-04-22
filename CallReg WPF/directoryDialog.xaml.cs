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
using System.Windows.Forms;
using System.IO;


namespace CallReg_WPF
{
    /// <summary>
    /// Interaction logic for directoryDialog.xaml
    /// </summary>
    public partial class directoryDialog : Window
    {
        public string resultDir;
        public directoryDialog()
        {
            InitializeComponent();
            try
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\location.cfg"))
                {
                    resultDir = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\location.cfg");
                    dirTextbox.Text = resultDir;
                }
                else
                {
                    //If the file was never created it asks for the user to make one. 
                    System.Windows.MessageBox.Show("Ainda nunca foi definida a pasta base, por favor selecione.");
                }
            }
            catch (Exception exc)
            {
                System.Windows.MessageBox.Show(exc.Message);
            }
        }

        private void bOpen_Click(object sender, RoutedEventArgs e)
        {
            string resultDir;
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                resultDir = dialog.SelectedPath;
            }
            dirTextbox.Text = resultDir;
        }

        private void bCommit_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\location.cfg", dirTextbox.Text);
            this.Close();
        }
    }
}
