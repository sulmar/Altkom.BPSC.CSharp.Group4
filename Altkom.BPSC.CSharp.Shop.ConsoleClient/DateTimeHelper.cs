using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.BPSC.CSharp.Shop.ConsoleClient
{
    public class DateTimeHelper
    {
        public static bool IsHoliday(DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday
                || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }
    }


    // Metoda rozszerzająca
    public static class DateTimeExtensions
    {
        public static bool IsHoliday(this DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday
                || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}
