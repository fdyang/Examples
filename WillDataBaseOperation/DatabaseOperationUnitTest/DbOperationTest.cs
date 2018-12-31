using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WillDataBaseOperation;

namespace DatabaseOperationUnitTest
{
    [TestClass]
    public class DbOperationTest
    {
        [TestMethod]
        public void InsertOneRow()
        {
            string sql = @"INSERT INTO ServerLog(DateTime,HttpState, FileType, FileName, FileSizeDownloaded, FileSizeOriginal)
                            VALUES ('2018/11/8', 'Skagen 21', 'Stavanger', '4006', 'Norway', '1234567');"; 

            SqlHelper.ExcuteSQL(sql); 

        }

        [TestMethod]
        public void BulkDataTable()
        {
            DataTable table = new DataTable();
            string[] columnNames = { "DateTime", "User", "HttpState", "FileType", "FileName",
                "FileSizeDownloaded", "FileSizeOriginal", "VisitorIP", "DomainName", "PortNumber",
                "NetworkAddress", "OriginalAddress", "VisitorIPorID", "IEandSystemInfo",
            };

            // Create heads for column.
            for (int i = 0; i < columnNames.Length; i++)
            {
                table.Columns.Add(columnNames[i]);
            }

            var row = table.NewRow();
            for (int i = 0; i < columnNames.Length; i++)
            {
                row[columnNames[i]] = "d" + i;
            }
            table.Rows.Add(row);

            SqlHelper.BulkToDB(table, "dbo.ServerLog", TimeSpan.FromSeconds(30)); 
        }
    }
}
