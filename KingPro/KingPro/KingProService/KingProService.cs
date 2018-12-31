using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillDataBaseOperation;
using WillDataProcess;

namespace KingProService
{
    partial class KingProService : ServiceBase
    {
        private Timer parseLogTimer;

        public KingProService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;
            TimerCallback timerParseLogDelegate = new TimerCallback(state => this.ParseLog());
            this.parseLogTimer = new Timer(timerParseLogDelegate, null, TimeSpan.FromSeconds(10), Properties.Settings.Default.RunningInterval);

            // Write an informational entry to the event log.
            this.eventLog.WriteEntry("KingProSource started.");
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            if (this.parseLogTimer != null)
            {
                this.parseLogTimer.Dispose();
                this.parseLogTimer = null; 
            }

            this.eventLog.WriteEntry("KingProSource stopped.");
        }

        private void ParseLog()
        {
            // Try to get the log files from specific path. 
            string logFilePath = Properties.Settings.Default.LogFilePath;
            if (string.IsNullOrEmpty(logFilePath))
            {
                return;
            }

            string[] allFiles = Directory.GetFiles(logFilePath, "*.log");

            DataTable table = new DataTable();
            LogParser parser = new LogParser();
            string[] splitStrings = new string[] { "||" };

            // Must match the columns in SQL server database. 
            string[] colNames = { "DateTime", "User", "HttpState", "FileType", "FileName",
                "FileSizeDownloaded", "FileSizeOriginal", "VisitorIP", "DomainName", "PortNumber",
                "NetworkAddress", "OriginalAddress", "VisitorIPorID", "IEandSystemInfo",
            };

            foreach (var file in allFiles)
            {
                // Only parse the files that in time frame. 
                var fileModifyTime = File.GetLastWriteTime(file);
                Debug.WriteLine("File modify time: {0}", fileModifyTime.ToString()); 
                if (fileModifyTime < DateTime.Today + TimeSpan.FromDays(1) || fileModifyTime > DateTime.Today)
                {
                    continue;
                }

                Debug.WriteLine("Parsing file: {0}", file.ToString()); 
                var t = parser.ParseWithSplit(file, colNames, splitStrings);
                table.Merge(t);
            }

            // Convert hex string to Chinese for display.
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

            // Save table into database.
            const string targetTable = "dbo.ServerLog";

            Debug.WriteLine("Writing table into database.");
            try
            {
                SqlHelper.BulkToDB(table, targetTable, Properties.Settings.Default.SaveDbTimeout);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("ERROR: {0}", ex.ToString());
                this.eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error); 
                return;
            }

            Debug.WriteLine("Writing table into database - DONE!!"); 
        }
    }
}
