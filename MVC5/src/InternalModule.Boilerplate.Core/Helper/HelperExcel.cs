using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InternalModule.Boilerplate.Core.Helper
{
    public class HelperExcel
    {
        public static uint GetRowNumber(string cellName)
        {
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(cellName);

            return uint.Parse(match.Value);
        }

        public static string GetColumnName(string cellName)
        {
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellName);

            return match.Value;
        }

        public static uint GetColumnNumber(string cellName)
        {
            return GetColumnName2Number(GetColumnName(cellName));
        }

        public static uint GetColumnName2Number(string columnName)
        {
            uint returnData = 0;
            string subString = columnName;
            uint multiply = 1;

            while (subString.Length > 0)
            {
                returnData = returnData + (((uint)Convert.ToChar(subString.Substring(subString.Length - 1, 1)) - 64) * multiply);
                subString = subString.Substring(0, subString.Length - 1);
                multiply = multiply * 26;
            }

            return returnData;
        }

        public static string GetColumnNumber2Name(uint columnNumber)
        {
            uint dividend = columnNumber;
            string returnData = String.Empty;
            uint modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                returnData = Convert.ToChar(65 + modulo).ToString() + returnData;
                dividend = ((dividend - modulo) / 26);
            }

            return returnData;
        }
    }
}
