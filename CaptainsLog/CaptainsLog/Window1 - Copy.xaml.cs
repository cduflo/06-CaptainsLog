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
using CaptainsLog.Core;
using System.Collections.ObjectModel;

namespace CaptainsLog
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window2 : Window
    {

        ObservableCollection<LogEntry> logEntries = new ObservableCollection<LogEntry>();
        static string temp;

        public Window2()
        {
            InitializeComponent();
            var mainWindow = Owner as MainWindow;
            textBoxEntry.Text = 
            textBoxTitle.Text = temp;// mainWindow.getTempTitle();
            textBoxEntry.Text = "hi";//mainWindow.getTempText();
        }

        public static void setTemp(string value)
        {
            temp = value;
        }

        private void buttonEntrySubmit_Click(object sender, RoutedEventArgs e)
        {

            var mainWindow = Owner as MainWindow;

            if (textBoxEntry.Text.Length > 0 && textBoxTitle.Text.Length > 0)
            {
                CaptainsLog.Core.LogEntry log = new LogEntry();
                log.Id = mainWindow.logEntries.Count() + 1;
                log.Title = textBoxTitle.Text;
                log.Text = textBoxEntry.Text;
                log.EntryDate = DateTime.Now;

                mainWindow.AddLog(log);
                
                this.Close();
            }
            else
            {
                MessageBox.Show("Please ensure both fields are populated and try again.");
            }
        }

        private void buttonEntryCancel_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEntry.Text.Length != 0 || textBoxTitle.Text.Length != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Unsaved changes. Are you sure you want to close this window?", "Unsaved Work", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
    }
}
