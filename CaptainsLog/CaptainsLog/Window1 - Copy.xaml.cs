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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {

        LogEntry tempObj { get; set; }
        int tempID;
        string origText;
        string origTitle;


        public Window2()
        {
            InitializeComponent();
        }

        public Window2(LogEntry name) : this()
        {
            this.tempObj = name;
            textBoxEntry.Text = tempObj.Text;
            textBoxTitle.Text = tempObj.Title;
            tempID = tempObj.Id;

            origText = tempObj.Text;
            origTitle = tempObj.Title;
        }

        private void buttonEntrySubmit_Click(object sender, RoutedEventArgs e)
        {

            var mainWindow = Owner as MainWindow;

            if (textBoxEntry.Text != origText || textBoxTitle.Text != origTitle)
            {
                CaptainsLog.Core.LogEntry log = new LogEntry();
                log.Id = tempID;
                log.Title = textBoxTitle.Text;
                log.Text = textBoxEntry.Text;
                log.UpdateDate = DateTime.Now;

                mainWindow.UpdateLog(log);
                mainWindow.RefreshGrid();
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void buttonEntryCancel_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEntry.Text != origText || textBoxTitle.Text != origTitle)
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
           // var mainWindow = Owner as MainWindow;
           // mainWindow.Activate();
        }
    }
}
