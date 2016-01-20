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

namespace CaptainsLog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<LogEntry>logEntries;

        public MainWindow()
        {
            InitializeComponent();
            logEntries = new ObservableCollection<LogEntry>();
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
    }
}
