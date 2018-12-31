using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using MVVMBase;
using WillDataBaseOperation;
using WillDataProcess;

namespace KingPro.ViewModel
{
    public class MainControlViewModel : ViewModelBase
    {
        private string logFilePath;

        private string runningLog; 

        private DataTable parseResultTable;

        private DataTable resultTableSaveToDatabase;

        private DateTime startDate;

        private DateTime endDate; 

        private ICommand runStartButton;

        private ICommand runFolderBrowserButton;

        private ICommand runSaveToDataBaseButton; 

        public MainControlViewModel()
        {
            this.startDate = DateTime.Now;
            this.endDate = DateTime.Now;
        }

        public string LogFilePath
        {
            get
            {
                return this.logFilePath;
            }

            set
            {
                if (this.logFilePath != value)
                {
                    this.logFilePath = value;
                    this.OnPropertyChanged("LogFilePath");
                }
            }
        }

        public string RunningLog
        {
            get
            {
                return this.runningLog;
            }

            set
            {
                if (this.runningLog != value)
                {
                    this.runningLog = value;
                    this.OnPropertyChanged("RunningLog");
                }
            }
        }

        public ICommand RunStartButton
        {
            get
            {
                if (this.runStartButton == null)
                {
                    this.runStartButton = new RelayCommand(
                        param =>
                        {
                            // Important: Kick off a new thread in order to not blocker the UI thread!!
                            Task.Factory.StartNew(this.Start);
                        },
                        null);
                }

                return this.runStartButton;
            }
        }

        public ICommand RunFolderBrowserButton
        {
            get
            {
                if (this.runFolderBrowserButton == null)
                {
                    this.runFolderBrowserButton = new RelayCommand(
                        param =>
                        {
                            // Important: Kick off a new thread in order to not blocker the UI thread!!
                            Task.Factory.StartNew(this.BrowseFolder);
                        },
                        null);
                }

                return this.runFolderBrowserButton;
            }
        }

        public ICommand RunSaveToDataBaseButton
        {
            get
            {
                if (this.runSaveToDataBaseButton == null)
                {
                    this.runSaveToDataBaseButton = new RelayCommand(
                        param =>
                        {
                            // Important: Kick off a new thread in order to not blocker the UI thread!!
                            Task.Factory.StartNew(this.SaveTableIntoDataBase);
                        },
                        null);
                }

                return this.runSaveToDataBaseButton;
            }
        }

        public DataTable ParseResultTable
        {
            get
            {
                return this.parseResultTable;
            }

            set
            {
                this.parseResultTable = value;
                this.OnPropertyChanged("ParseResultTable");
            }
        }

        public DateTime StartDate
        {
            get
            {
                return this.startDate;
            }

            set
            {
                this.startDate = value;
                this.OnPropertyChanged("StartDate"); 
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this.endDate;
            }

            set
            {
                this.endDate = value;
                this.OnPropertyChanged("EndDate");
            }
        }

        private void Start()
        {
            if (string.IsNullOrEmpty(this.logFilePath))
            {
                return;
            }

            string[] allFiles = Directory.GetFiles(this.logFilePath, "*.log");

            DataTable table = new DataTable();
            LogParser parser = new LogParser();
            string[] splitStrings = new string[] { "||" };

            // Must match the columns in SQL server database. 
            string[] colNames = { "DateTime", "User", "HttpState", "FileType", "TBD0", "FileName",
                "FileSizeDownloaded", "TBD1", "FileSizeOriginal", "VisitorIP", "DomainName", "PortNumber",
                "NetworkAddress", "OriginalAddress", "VisitorIPorID", "IEandSystemInfo", "HttpStateCode", "GET", "TBD2", "TBD3",
            };

            foreach (var file in allFiles)
            {
                // Only parse the files that in time frame. 
                var fileModifyTime = File.GetLastWriteTime(file);
                if (fileModifyTime < this.startDate || fileModifyTime > this.endDate)
                {
                    continue;
                }

                this.RunningLog = "Parsing Log File: " + file; 
                var t = parser.ParseWithSplit(file, colNames, splitStrings);
                this.RunningLog = "Parsing Log File - Done";
                table.Merge(t);
            }

            this.resultTableSaveToDatabase = table;

            // Convert hex string to Chinese for display.
            // Trim table before saving to database.
            string chnFileName = string.Empty;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["User"].ToString().Length > 300)
                {
                    table.Rows[i]["User"] = table.Rows[i]["User"].ToString().Substring(0, 300); 
                }

                if (table.Rows[i]["HttpState"].ToString().Length > 300)
                {
                    table.Rows[i]["HttpState"] = table.Rows[i]["HttpState"].ToString().Substring(0, 300);
                }

                if (table.Rows[i]["FileType"].ToString().Length > 300)
                {
                    table.Rows[i]["FileType"] = table.Rows[i]["FileType"].ToString().Substring(0, 300);
                }

                if (table.Rows[i]["FileSizeDownloaded"].ToString().Length > 300)
                {
                    table.Rows[i]["FileSizeDownloaded"] = table.Rows[i]["FileSizeDownloaded"].ToString().Substring(0, 300);
                }

                if (table.Rows[i]["FileSizeOriginal"].ToString().Length > 300)
                {
                    table.Rows[i]["FileSizeOriginal"] = table.Rows[i]["FileSizeOriginal"].ToString().Substring(0, 300);
                }

                if (table.Rows[i]["FileSizeDownloaded"].ToString().Length > 300)
                {
                    table.Rows[i]["FileSizeDownloaded"] = table.Rows[i]["FileSizeDownloaded"].ToString().Substring(0, 300);
                }

                if (table.Rows[i]["VisitorIP"].ToString().Length > 300)
                {
                    table.Rows[i]["VisitorIP"] = table.Rows[i]["VisitorIP"].ToString().Substring(0, 300);
                }

                if (table.Rows[i]["DomainName"].ToString().Length > 300)
                {
                    table.Rows[i]["DomainName"] = table.Rows[i]["DomainName"].ToString().Substring(0, 300);
                }

                if (table.Rows[i]["PortNumber"].ToString().Length > 300)
                {
                    table.Rows[i]["PortNumber"] = table.Rows[i]["PortNumber"].ToString().Substring(0, 300);
                }

                if (table.Rows[i]["VisitorIPorID"].ToString().Length > 300)
                {
                    table.Rows[i]["VisitorIPorID"] = table.Rows[i]["VisitorIPorID"].ToString().Substring(0, 300);
                }

                if (table.Rows[i]["IEandSystemInfo"].ToString().Length > 300)
                {
                    table.Rows[i]["IEandSystemInfo"] = table.Rows[i]["IEandSystemInfo"].ToString().Substring(0, 300);
                }

                if (table.Rows[i]["DateTime"].ToString().Length > 300)
                {
                    table.Rows[i]["DateTime"] = table.Rows[i]["DateTime"].ToString().Substring(0, 100);
                }

                if (table.Rows[i]["TBD0"].ToString().Length > 100)
                {
                    table.Rows[i]["TBD0"] = table.Rows[i]["TBD0"].ToString().Substring(0, 100);
                }


                if (table.Rows[i]["TBD1"].ToString().Length > 100)
                {
                    table.Rows[i]["TBD1"] = table.Rows[i]["TBD1"].ToString().Substring(0, 100);
                }


                if (table.Rows[i]["TBD2"].ToString().Length > 100)
                {
                    table.Rows[i]["TBD2"] = table.Rows[i]["TBD2"].ToString().Substring(0, 100);
                }


                if (table.Rows[i]["TBD3"].ToString().Length > 100)
                {
                    table.Rows[i]["TBD3"] = table.Rows[i]["TBD3"].ToString().Substring(0, 100);
                }

                if (table.Rows[i]["GET"].ToString().Length > 100)
                {
                    table.Rows[i]["GET"] = table.Rows[i]["GET"].ToString().Substring(0, 100);
                }

                if (table.Rows[i]["HttpStateCode"].ToString().Length > 50)
                {
                    table.Rows[i]["HttpStateCode"] = table.Rows[i]["HttpStateCode"].ToString().Substring(0, 50);
                }

                chnFileName = table.Rows[i]["FileName"].ToString();
                table.Rows[i]["FileName"] = ParserHelper.ConvertHexStringToNormalChineseString(chnFileName);
            }

            this.ParseResultTable = table;
        }

        private void BrowseFolder()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                Thread t = new Thread(() => dialog.ShowDialog());
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                t.Join();

                var path = dialog.SelectedPath;
                if (!string.IsNullOrEmpty(path))
                {
                    this.LogFilePath = path;
                }
            }
        }

        private void SaveTableIntoDataBase()
        {
            if (this.resultTableSaveToDatabase != null)
            {
                const string targetTable = "dbo.ServerLog";

                // Debug. 
                //foreach(DataRow row in this.resultTableSaveToDatabase.Rows)
                //{
                //    for(int i = 0; i < row.ItemArray.Length; i++)
                //    {
                //        Debug.WriteLine(string.Format("{0} - {1}", i, row.ItemArray[i])); 
                //    }
                //}

                this.RunningLog = "Saving table to database.";
                try
                {
                    SqlHelper.BulkToDB(this.resultTableSaveToDatabase, targetTable, TimeSpan.FromMinutes(5));
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.ToString()); 
                    this.RunningLog = "ERROR: " + ex.ToString();
                    return; 
                }

                this.RunningLog = "Saving table to database - Done!";
            }
        }

    }
}
