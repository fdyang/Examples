using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using DataGridOperation.ViewModel;
using FolderBrowser.ViewModel;
using WillDataProcess;
using WillDataBaseOperation;

namespace KingPro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.ResetUI(); 
        }

        private void ResetUI()
        {
            this.LogFileName.Text = string.Empty;
            this.ParseStatus.Text = string.Empty; 
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            string logPath = ((FolderBrowserViewModel)this.FolderView.DataContext).LogDirectory;

            // Get all log files.
            if (string.IsNullOrEmpty(logPath))
            {
                return;
            }

            string[] allFiles = Directory.GetFiles(logPath, "*.log");
            DataTable table = new DataTable();
            LogParser parser = new LogParser();
            string[] splitStrings = new string[] { "||-", "||" };
            
            // Must match the columns in SQL server database. 
            string[] colNames = { "DateTime", "User", "HttpState", "FileType", "FileName",
                "FileSizeDownloaded", "FileSizeOriginal", "VisitorIP", "DomainName", "PortNumber",
                "NetworkAddress", "OriginalAddress", "VisitorIPorID", "IEandSystemInfo",
            };

            // Update log file name in UI. 
            var task = Task.Factory.StartNew(() =>
            {
                int i = 0; 
                foreach (var file in allFiles)
                {
                    this.MainGrid.Dispatcher.Invoke(() =>
                    {
                        this.LogFileName.Text = file;
                        this.ParseStatus.Text = string.Format("{0}/{1}", ++i, allFiles.Length); 
                        Thread.Sleep(50);
                    });

                    var t = parser.ParseWithSplit(file, colNames, splitStrings);
                    table.Merge(t); 
                }

                // Using dispatcher to update UI from non-UI thread.
                this.DataGridView.Dispatcher.Invoke(() =>
                {
                    // Set to data grid view.
                    ((DataGridOperationViewModel)this.DataGridView.DataContext).Table = table;
                });

            });
        }

        private void SaveToDb_Click(object sender, RoutedEventArgs e)
        {
            const string targetTable = "dbo.ServerLog"; 
            DataTable data = ((DataGridOperationViewModel)this.DataGridView.DataContext).Table;

            SqlHelper.BulkToDB(data, targetTable);
            MessageBox.Show("Complete!");
            
        }

    }

}
