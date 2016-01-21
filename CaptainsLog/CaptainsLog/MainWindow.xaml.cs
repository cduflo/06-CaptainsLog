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
        //Define global variables
        public ObservableCollection<LogEntry> logEntries;
        string filename = "C:\\dev\\origin\\06-CaptainsLog\\CaptainsLog\\TextFile1.txt";

        public MainWindow()
        {
            InitializeComponent();
            string cont = readFile(filename);

            //Load logEntries with log data held in filename, generate blank OC otherwise
            if (readFile(filename).Length < 10)
            {
                logEntries = new ObservableCollection<LogEntry>();
            }
            else
            {
                logEntries = new ObservableCollection<LogEntry>(DeSerJSON(filename));
            }

            //Have GridData display logEntries data
            gridLogEntries.ItemsSource = logEntries;
        }


        //Add Button launches new window with ownership assigned*
        private void buttonAddEntry_Click(object sender, RoutedEventArgs e)
        {
            Window1 entryWindow = new Window1();
            entryWindow.Owner = this;
            entryWindow.Show();
        }

        //Remove Button removes last Entry from logEntries
        private void buttonRemoveEntry_Click(object sender, RoutedEventArgs e)
        {
            if (logEntries.Count() > 0)
            {
                logEntries.Remove(logEntries[logEntries.Count() - 1]);
            }
        }

        //Grid DoubleClick jumps to new window for editing
        public void myDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (gridLogEntries.SelectedItem == null)
            { return; }
            LogEntry selectedLog = gridLogEntries.SelectedItem as LogEntry;

            Window2 editWindow = new Window2(selectedLog);
            editWindow.Owner = this;
            editWindow.Show();
        }

        //Public Methods so that Window1 can access/manipulate data on MainWindow
        public void AddLog(LogEntry log)
        {
            logEntries.Add(log);
        }

        //Window Closing event handler that saves logEntries data to local file, which will load on next startup
        public void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            writeFile(SerJSON(logEntries), filename);
        }

        //JSON Serialize/Deserialize Methods
        public string SerJSON(object data)
        {
            string serialized = JsonConvert.SerializeObject(data);
            return serialized;
        }

        public ObservableCollection<LogEntry> DeSerJSON(string fn)
        {
            string text = readFile(fn);
            ObservableCollection<LogEntry> imported = JsonConvert.DeserializeObject<ObservableCollection<LogEntry>>(text);
            return imported;
        }

        //File Read/Write Methods
        public void writeFile(string logs, string fn)
        {
            StreamWriter objWriter;
            objWriter = new System.IO.StreamWriter(fn);
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
