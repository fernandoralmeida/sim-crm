using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace Sim.UI.Web.Functions
{
    public class Dates
    {
        public Dates()
        {

        }

        public Dates(int month, int year)
        {
            if (getWeekdatesandDates(month, year) != null)
            {
                WeekDateStart = getWeekdatesandDates(month, year).FirstOrDefault().Day.ToString();
                WeekDateEnd = getWeekdatesandDates(month, year).LastOrDefault().Day.ToString();
            }
        }

        public string? WeekDateStart
        {
            get; private set;
        }

        public string? WeekDateEnd
        {
            get; private set;
        }

        private List<DateTime> getWeekdatesandDates(int Month, int Year)
        {
            List<DateTime> weekdays = new List<DateTime>();

            DateTime firstOfMonth = new DateTime(Year, Month, 1);

            DateTime currentDay = firstOfMonth;
            while (firstOfMonth.Month == currentDay.Month)
            {
                DayOfWeek dayOfWeek = currentDay.DayOfWeek;
                if (dayOfWeek != DayOfWeek.Saturday && dayOfWeek != DayOfWeek.Sunday)
                    weekdays.Add(currentDay);

                currentDay = currentDay.AddDays(1);
            }

            return weekdays;
        }
    }

    static class DateTimeExtensions
    {
        public static string ToMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }

        public static string ToShortMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);
        }
    }
}
