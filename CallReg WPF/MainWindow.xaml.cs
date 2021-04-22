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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;

namespace CallReg_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //At application start it checks if there is the config with the desired user location.
            Data d = new Data();
            d.saveDirCheck();
        }
        //deprecated
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        //deprecated
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Normal)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }

        }

        //b_Commit is where most action goes on. Its the function that checks what has been done and saves the info to the files.
        private void bCommit_Click(object sender, RoutedEventArgs e)
        {
            Data d = new Data();
            d.dataInit();
            try
            {
                //This gets the user inputed data and makes it into a List.
                var currentData = new List<DataCollection>
                {
                    new DataCollection()
                    {
                        id=int.Parse(idTextbox.Text),
                        callId=int.Parse(nrTextbox.Text),
                        name=nameTextbox.Text,
                        addressVer=(bool)addressCheckbox.IsChecked,
                        situation=new TextRange(situationTextBox.Document.ContentStart,
                        situationTextBox.Document.ContentEnd).Text,
                        olt=oltTextbox.Text,
                        cell=cellTextbox.Text,
                        techSee=(bool)techseeCheckbox.IsChecked,
                        appSmartrouter=(bool)smartrouterCheckbox.IsChecked,
                        icr=(bool)icrCheckbox.IsChecked
                    }
                };

                //This takes the data and formats it to go into the file.
                List<string> textToWrite = new List<string>();
                foreach (DataCollection a in currentData)
                {
                    textToWrite.Add("ID: " + a.id.ToString());
                    textToWrite.Add("Nº de od liga: " + a.callId.ToString());
                    textToWrite.Add("Nome: " + a.name);
                    textToWrite.Add("Validação morada: " + a.addressVer.ToString());
                    textToWrite.Add("Situação: " + a.situation);
                    textToWrite.Add("Central/Olt: " + a.olt);
                    textToWrite.Add("Célula: " + a.cell);
                    textToWrite.Add("TechSee: " + a.techSee.ToString());
                    textToWrite.Add("App SmartRouter: " + a.appSmartrouter.ToString());
                    textToWrite.Add("Inquerito ICR: " + a.icr.ToString() + "\n");
                }

                //Check if there is a folder for the month in question
                if (Directory.Exists(d.saveDirMonth))
                {

                }
                //Create that folder if there isn't.
                else
                {
                    Directory.CreateDirectory(d.saveDirMonth);

                }

                //Temporary list that will contain everything that will go into the file.
                List<string> tmpFile = new List<string>();

                //Checks if the file exists, if it does, it'll read it and then concat the new info before writing it...
                if(File.Exists(d.saveDirFile + ".txt"))
                {
                    tmpFile.AddRange(File.ReadAllLines(d.saveDirFile + ".txt"));
                    tmpFile.Add("------------------------------------------------------------------------------------- \n");
                    tmpFile.AddRange(textToWrite);
                }
                //...if there isn't one, or after it has got the info from the old file, it writes the new file with all the new info. 
                MessageBox.Show(d.saveDirFile);
                File.WriteAllLines(d.saveDirFile + ".txt", tmpFile);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //The settings button opens the directory dialog since this is the only setting that the user will access.
        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            directoryDialog directoryWindow = new directoryDialog();
            directoryWindow.Show();
        }
    }

    //DataCollection is a collection with all the info inputed by the user. 
    public class DataCollection
    {
        public int id { get; set; }
        public int callId { get; set; }
        public string name { get; set; }
        public bool addressVer { get; set; }
        public string situation { get; set; }
        public string olt { get; set; }
        public string cell { get; set; }
        public bool techSee { get; set; }
        public bool appSmartrouter { get; set; }
        public bool icr { get; set; }

    }

    //The Data class is where backend data get manipulated and stored.
    public class Data
    {
        //these are locally used variables.
        private static DateTime currentDateLocal = DateTime.Now;
        private static string saveDirLocal = AppDomain.CurrentDomain.BaseDirectory + @"\location.cfg";
        private static string saveDirMonthLocal;
        //Temp fix. ^

        //These are variables accessible wherever in the mainWindow workspace
        public DateTime currentDate = DateTime.Now;
        public string mainDir = AppDomain.CurrentDomain.BaseDirectory;
        public string saveDir = AppDomain.CurrentDomain.BaseDirectory + @"\location.cfg";
        public string saveDirMonth;
        public string saveDirFile = saveDirMonthLocal + @"/" + currentDateLocal.Day.ToString();
        
        //This function gets called to verify if there is a file with the default save location and sets some local variables
        public void dataInit()
        {
            saveDirCheck();
            if(File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\location.cfg"))
            {
                saveDirMonthLocal = File.ReadAllText(saveDirLocal) + @"/" + currentDateLocal.ToString("MMMM");
                saveDirMonth = File.ReadAllText(saveDirLocal) + @"/" + currentDateLocal.ToString("MMMM"); 
            }
        }
        //This function checks if there is 
        public void saveDirCheck()
        {
            try
            {
                if (File.Exists(saveDir))
                {

                }
                else
                {
                    directoryDialog directoryWindow = new directoryDialog();
                    directoryWindow.Show();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
