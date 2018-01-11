using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Aula02.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveSpaces(this string s)
        {
            return s.Replace(" ",string.Empty);
        }

        public static DateTime? AsDateTime(this string data, string format)
        {
            DateTime d;
            if (DateTime.TryParseExact(data,format,CultureInfo.CurrentCulture, DateTimeStyles.None, out d))
            {
                return d;
            }
            return null;
        }
    }
}
