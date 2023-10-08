using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOXClient
{
    internal static class Extensions
    {
        private static CultureInfo ENcultureInfo = new CultureInfo("en-US");

        public static string ToLowerEN(this string value)
        {
            return value.ToLower(ENcultureInfo);
        }
        public static string ToUpperEN(this string value)
        {
            return value.ToUpper(ENcultureInfo);
        }
    }
}
