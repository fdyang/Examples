using System;
using System.Data;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WillDataProcess;

namespace DataProcessTest
{
    [TestClass]
    public class LogParserTest
    {
        [TestMethod]
        public void LoadLogFileAndParseTest()
        {
            string logFile = @"C:\Temp\KingPro\TestLog.log";
            //string pattern = @"";
            string[] splitStrings = new string[]{"||-", "||" }; 
            string[] colNames = { "Date Time", "User", "Http State", "File Type", "File Name",
                "File Size(Downloaded)", "File Size (Original)", "Visitor IP", "Domain Name", "Port Number",
                "Network Address", "Original Address", "Visitor IP or ID", "IE and System Info",
            };

            LogParser parser = new LogParser();
            DataTable result = parser.ParseWithSplit(logFile, colNames, splitStrings); 

            foreach(DataRow row in result.Rows)
            {
                foreach (string colName in colNames)
                {
                    string content = row[colName].ToString();
                    if (colName == "File Name")
                    {
                        content = ParserHelper.ConvertHexStringToNormalChineseString(content); 
                    }

                    Debug.Write(content);
                    Debug.Write(",");
                    Debug.Write(Environment.NewLine); 
                }
            }
        }
    }
}
