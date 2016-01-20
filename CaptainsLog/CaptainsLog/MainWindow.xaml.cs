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
using CaptainsLog.Core;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace CaptainsLog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<LogEntry> logEntries;
        string filename = "C:\\dev\\origin\\06-CaptainsLog\\CaptainsLog\\data\\Logs.txt";

        public MainWindow()
        {
            //Read Logs File

            //Deserialize JSON
           // imported = DeSerJSON();
            //Load JSON to logEntries
            InitializeComponent();
            logEntries = new ObservableCollection<LogEntry>(DeSerJSON());
            gridLogEntries.ItemsSource = logEntries;
        }

        private void buttonAddEntry_Click(object sender, RoutedEventArgs e)
        {
            Window1 entryWindow = new Window1();
            entryWindow.Owner = this;
            entryWindow.Show();
        }

        private void buttonRemoveEntry_Click(object sender, RoutedEventArgs e)
        {
            if (logEntries.Count() > 0)
            {
                logEntries.Remove(logEntries[logEntries.Count() - 1]);
            }
        }

        public void AddLog(LogEntry log)
        {
            logEntries.Add(log);
        }

        public int entryCount()
        {
            return logEntries.Count();
        }

        public void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Call Serialize JSON
                     // string jsonString = SerializeJSon<ObservableCollection<LogEntry>>(logEntries);
            writeFile(SerJSON());
            MessageBox.Show(SerJSON());
        }
        /*
        public static string SerializeJSon<T>(T t)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
            DataContractJsonSerializerSettings s = new DataContractJsonSerializerSettings();
            ds.WriteObject(stream, t);
            string jsonString = Encoding.UTF8.GetString(stream.ToArray());
            stream.Close();
            return jsonString;
        }*/

        public string SerJSON()
        {
            string serialized = JsonConvert.SerializeObject(logEntries);
            return serialized;
        }

         public ObservableCollection<LogEntry> DeSerJSON()
        //public void DeSerJSON()
        {
            string text = readFile(filename);
ObservableCollection<LogEntry>  imported = JsonConvert.DeserializeObject<ObservableCollection<LogEntry>>(text);
          return imported;
        }


        public void writeFile(string logs)
        {
            StreamWriter objWriter;
            objWriter = new System.IO.StreamWriter(filename);
            objWriter.Write(logs);
            objWriter.Close();
        }

        public string readFile(string fn)
        {
            string jsontext = "";
            System.IO.StreamReader objReader;
            objReader = new System.IO.StreamReader(fn);
            do
            {
                jsontext += objReader.ReadLine() + "\r\n";
            }
            while (objReader.Peek() != -1);
            objReader.Close();
            return jsontext;
        }
    }
}
