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
        public string defaultLocation = AppDomain.CurrentDomain.BaseDirectory + @"\location.cfg";
        public directoryDialog()
        {
            InitializeComponent();
            //At the start of the dialog it looks up the cfg file. 
            try
            {
                if (File.Exists(defaultLocation))
                {
                    resultDir = File.ReadAllText(defaultLocation);
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

        //The open button opens a folder selection dialog.
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

        //When you commit the app gets the location you entered and changes the data, or not.
        private void bCommit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(defaultLocation))
                {
                    if (File.ReadAllText(defaultLocation) == dirTextbox.Text)
                    {
                        //nothing
                    }
                    else
                    {
                        File.WriteAllText(defaultLocation, dirTextbox.Text);
                    }
                }
                else
                {
                    //After creating the file it calls the function in Data class to correctly populate the variables.
                    File.WriteAllText(defaultLocation, dirTextbox.Text);
                    Data mw = new Data();
                    mw.dataInit();
                    
                }
                    
            }
            catch(Exception exc)
            {
                System.Windows.MessageBox.Show(exc.Message);
            }
            this.Close();
        }
    }
}
