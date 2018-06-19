using System;
using System.Globalization;

namespace ACP.Commons
{
    public class DateHelper
    {
        public static DateTime ConvertToUKDateTime(string date) => DateTime.ParseExact(date, "dd-MM-yyyy HH:mm:ss", new CultureInfo("en-UK"));

        public static DateTime ConvertToUKDateTime(DateTime date) => Convert.ToDateTime(date, new CultureInfo("en-UK"));
    }
}
