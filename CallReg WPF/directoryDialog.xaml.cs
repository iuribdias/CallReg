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


namespace CallReg_WPF
{
    /// <summary>
    /// Interaction logic for directoryDialog.xaml
    /// </summary>
    public partial class directoryDialog : Window
    {
        public directoryDialog()
        {
            InitializeComponent();
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
            
        }
    }
}
