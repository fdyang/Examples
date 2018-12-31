using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WillDataProcess
{
    public class LogParser
    {
        public LogParser()
        {

        }

        /// <summary>
        /// Parse the file and save to a DataTable.
        /// </summary>
        /// <param name="columnNames">The names for column.</param>
        /// <param name="fileName">The file name with full path.</param>
        /// <param name="parseRegexPattern">The regex pattern to parse.</param>
        /// <returns></returns>
        public DataTable ParseWithRegex(string fileName, string[] columnNames, string parseRegexPattern)
        {
            // Validate the file exists or not.
            if(!File.Exists(fileName))
            {
                throw new FileNotFoundException(fileName); 
            }

            var table = new DataTable(); 
            
            // Create heads for column.
            for(int i = 0; i < columnNames.Length; i++)
            {
                table.Columns.Add(columnNames[i]); 
            }

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line; 
                while((line = reader.ReadLine())!= null)
                {
                    Match m = Regex.Match(line, parseRegexPattern); 

                    if (m.Success)
                    {
                        var row = table.NewRow(); 
                        for(int i = 0; i < columnNames.Length; i++)
                        {
                            string variableName = "a" + i;   // Need to find a better way to remove the hard code here.
                            row[columnNames[i]] = m.Groups[variableName].Value; 
                        }
                        table.Rows.Add(row); 
                    }
                }
            }

            return table; 
        }

        /// <summary>
        /// Parse the file and save to a DataTable.
        /// </summary>
        /// <param name="columnNames">The names for column.</param>
        /// <param name="fileName">The file name with full path.</param>
        /// <param name="splitStrings">The split string.</param>
        /// <returns>The parse result.</returns>
        public DataTable ParseWithSplit(string fileName, string[] columnNames, string[] splitStrings)
        {
            // Validate the file exists or not.
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(fileName);
            }

            var table = new DataTable();

            // Create heads for column.
            for (int i = 0; i < columnNames.Length; i++)
            {
                table.Columns.Add(columnNames[i]);
            }

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] splits = line.Split(splitStrings, StringSplitOptions.RemoveEmptyEntries);

                    if (splits.Length > 0 )
                    {
                        var row = table.NewRow();
                        for (int i = 0; i < columnNames.Length; i++)
                        {
                            if (i < splits.Length)
                            {
                                row[columnNames[i]] = splits[i]; 
                            }
                            else
                            {
                                row[columnNames[i]] = null; 
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
            }

            return table;
        }
    }
}
