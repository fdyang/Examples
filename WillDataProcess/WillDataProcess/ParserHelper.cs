using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WillDataProcess
{
    public static class ParserHelper
    {
        public static string ConvertByteArrayToString(byte[] byteArray)
        {
            return System.Text.Encoding.Default.GetString(byteArray); 
        }

        public static byte[] ConvertHexStringArrayToByteArray(string[] hexStringArray)
        {
            List<byte> byteList = new List<byte>();
            foreach(var hex in hexStringArray)
            {
                byteList.Add(Convert.ToByte(hex, 16)); 
            }
            return byteList.ToArray(); 
        }

        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            byte[] data = new byte[hexString.Length / 2];
            for (int index = 0; index < data.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                data[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return data;
        }

        public static string ConvertHexStringToNormalChineseString(string input)
        {
            // Chinese string has two bytes. Encoding = GBK
            string pattern = @"(?<unicode>(\\x[0-9A-Fa-f]{2}){2})|(?<ascii>[\w\.^]+)";
            MatchCollection matches = Regex.Matches(input, pattern);
            StringBuilder output = new StringBuilder();
            string temp = string.Empty;
            for (int i = 0; i < matches.Count; i++)
            {
                temp = matches[i].Value;
                if (temp.StartsWith(@"\x"))
                {
                    temp = temp.Replace(@"\x", string.Empty);
                    temp = Encoding.GetEncoding("GBK").GetString(HexStringToByteArray(temp));
                    output.Append(temp);
                }
                else
                {
                    output.Append(temp);
                }
            }

            return output.ToString(); 
        }

        public static bool OnlyHexInString(string inputString)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(inputString, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
