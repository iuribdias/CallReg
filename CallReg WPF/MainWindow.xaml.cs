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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

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

        private void bCommit_Click(object sender, RoutedEventArgs e)
        {
            var currentData = new List<Data>
            {
                new Data(){
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
            List<string> textToWrite = new List<string>();
            foreach(Data a in currentData)
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
                textToWrite.Add("Inquerito ICR: " + a.icr.ToString());
            }
            File.WriteAllLines(@"C:\Users\Iuri Dias\Desktop\test.txt", textToWrite);
        }
    }
    public class Data
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
}
