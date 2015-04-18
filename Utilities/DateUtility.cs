using System;
using System.Collections.Generic;
using System.Globalization;

namespace Karasoft.Mvc.Utilities
{
    public class DateUtility
    {
        public static Age FormatAgeAr(DateTime start, DateTime end)
        {
            System.Globalization.UmAlQuraCalendar arCal = new System.Globalization.UmAlQuraCalendar();
            // Compute the difference between start 
            //year and end year. 

            int years = arCal.GetYear(end) - arCal.GetYear(start);

            int months = 0;

            int days = 0;

            // Check if the last year was a full year. 

            if (end < arCal.AddYears(start, years) && years != 0)
            {

                --years;

            }

            start = arCal.AddYears(start, years);

            // Now we know start <= end and the diff between them

            // is < 1 year. 

            if (arCal.GetYear(start) == arCal.GetYear(end))
            {

                months = arCal.GetMonth(end) - arCal.GetMonth(start);

            }

            else
            {

                months = (12 - arCal.GetMonth(start)) + arCal.GetMonth(end);

            }

            // Check if the last month was a full month.

            if (end < arCal.AddMonths(start, months) && months != 0)
            {

                --months;

            }

            start = arCal.AddMonths(start, months);

            // Now we know that start < end and is within 1 month

            // of each other. 

            days = (end - start).Days;
            Age ocsa = new Age();
            ocsa.day = days;
            ocsa.month = months;
            ocsa.year = years;
            return ocsa;

        }
        public static Age FormatAge(DateTime start, DateTime end)
        {
            System.Globalization.GregorianCalendar arCal = new System.Globalization.GregorianCalendar();
            // Compute the difference between start 
            //year and end year. 

            int years = arCal.GetYear(end) - arCal.GetYear(start);

            int months = 0;

            int days = 0;

            // Check if the last year was a full year. 

            if (end < arCal.AddYears(start, years) && years != 0)
            {

                --years;

            }

            start = arCal.AddYears(start, years);

            // Now we know start <= end and the diff between them

            // is < 1 year. 

            if (arCal.GetYear(start) == arCal.GetYear(end))
            {

                months = arCal.GetMonth(end) - arCal.GetMonth(start);

            }

            else
            {

                months = (12 - arCal.GetMonth(start)) + arCal.GetMonth(end);

            }

            // Check if the last month was a full month.

            if (end < arCal.AddMonths(start, months) && months != 0)
            {

                --months;

            }

            start = arCal.AddMonths(start, months);

            // Now we know that start < end and is within 1 month

            // of each other. 

            days = (end - start).Days;
            Age ocsa = new Age();
            ocsa.day = days;
            ocsa.month = months;
            ocsa.year = years;
            return ocsa;

        }
        public static DateTime ToUmlqura(DateTime date)
        {
            int y = ArabicCalendar.GetYear(date);
            int mo = ArabicCalendar.GetMonth(date);
            int day = ArabicCalendar.GetDayOfMonth(date);
            int h = ArabicCalendar.GetHour(date);
            int min = ArabicCalendar.GetMinute(date);
            int sec = ArabicCalendar.GetSecond(date);
            int ms = (int)ArabicCalendar.GetMilliseconds(date);
            return new DateTime(y, mo, day, h, min, sec, ms);
            // return ArabicCalendar.ToDateTime(y, mo, day, h, min, sec, ms);
        }
        public static DateTime GetFirstDateOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.GetCultureInfo("ar-SA");
            return GetFirstDateOfWeek(dayInWeek, defaultCultureInfo);
        }
        public static DateTime GetFirstDateOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            return GetFirstDateOfWeek(dayInWeek, cultureInfo.DateTimeFormat.FirstDayOfWeek);
        }
        public static DateTime GetFirstDateOfWeek(DateTime dayInWeek, DayOfWeek fday)
        {
            DayOfWeek firstDay = fday;// cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDateInWeek = dayInWeek.Date;
            while (firstDateInWeek.DayOfWeek != firstDay)
                firstDateInWeek = firstDateInWeek.AddDays(-1);
            return firstDateInWeek;
        }
        public static int GetWeekNumber(DateTime dtPassed)
        {
            System.Globalization.CultureInfo _Culture = new System.Globalization.CultureInfo("ar-SA", false);
            int weekNum = _Culture.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, _Culture.DateTimeFormat.FirstDayOfWeek);
            return weekNum;
        }
        public static DateTime GetStartWeekDate(DateTime startdate)
        {
            int diffs = 0;
            int Todays = (int)startdate.DayOfWeek;
            int firstdateofw = (int)DayOfWeek.Saturday;

            diffs = Todays - firstdateofw;

            return startdate.AddDays(-(((int)Todays) + 1));
        }
        public static string ConvertToHijriDate(DateTime date)
        {
            System.Globalization.CultureInfo _Culture = new System.Globalization.CultureInfo("ar-SA", false);
            _Culture.DateTimeFormat.Calendar = new System.Globalization.UmAlQuraCalendar();
            return date.ToString("yyyy/M/d", _Culture.DateTimeFormat);
        }
        public static string ConvertToHijriDate(DateTime date, string format)
        {
            try
            {
                System.Globalization.CultureInfo _Culture = new System.Globalization.CultureInfo("ar-SA", false);
                _Culture.DateTimeFormat.Calendar = new System.Globalization.UmAlQuraCalendar();
                return date.ToString(format, _Culture.DateTimeFormat);
            }
            catch (System.ArgumentOutOfRangeException ex)
            {
                return "Error";
            }
        }
        public static string ConvertToHijriDate(DateTime? date)
        {
            try
            {
                System.Globalization.CultureInfo _Culture = new System.Globalization.CultureInfo("ar-SA", false);
                _Culture.DateTimeFormat.Calendar = new System.Globalization.UmAlQuraCalendar();
                if (date.HasValue)
                    return date.Value.ToString(_Culture.DateTimeFormat.LongDatePattern, _Culture.DateTimeFormat);
                else return string.Empty;
            }
            catch (System.ArgumentOutOfRangeException ex)
            {
                return "Error";
            }
        }
        public static string ConvertToHijriDate(DateTime? date, string format)
        {
            try
            {
                System.Globalization.CultureInfo _Culture = new System.Globalization.CultureInfo("ar-SA", false);
                _Culture.DateTimeFormat.Calendar = new System.Globalization.UmAlQuraCalendar();
                if (date.HasValue)
                    return date.Value.ToString(format, _Culture.DateTimeFormat);
                else return string.Empty;
            }
            catch (System.ArgumentOutOfRangeException ex)
            {
                return "Error";
            }
        }

        public static DateTime ConvertHijriToGregorian(int day, int month, int year)
        {
            System.Globalization.CultureInfo _Culture = new System.Globalization.CultureInfo("ar-SA", false);
            var DefualtCalendar = new System.Globalization.UmAlQuraCalendar();
            _Culture.DateTimeFormat.Calendar = DefualtCalendar;
            //DefualtCalendar.ToDateTime
            DateTime tempdate = new DateTime(year, month, day, DefualtCalendar);
            return tempdate;
           // return DateTime.ParseExact(tempdate.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US")), _Culture.DateTimeFormat.GetAllDateTimePatterns(), new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.AllowWhiteSpaces | System.Globalization.DateTimeStyles.AllowInnerWhite);
        }

        public static string ConvertToDayName(DateTime? date)
        {
            if (date.HasValue)
                return ConvertToDayName(date.Value);
            else return string.Empty;
        }
        public static string ConvertToDayName(DateTime date)
        {
            try
            {
                System.Globalization.CultureInfo _Culture = new System.Globalization.CultureInfo("ar-SA", false);
                _Culture.DateTimeFormat.Calendar = new System.Globalization.UmAlQuraCalendar();

                return _Culture.DateTimeFormat.GetDayName(date.DayOfWeek);

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string ConvertToMonthName(DateTime date)
        {
            try
            {
                System.Globalization.CultureInfo _Culture = new System.Globalization.CultureInfo("ar-SA", false);
                _Culture.DateTimeFormat.Calendar = new System.Globalization.UmAlQuraCalendar();

                return _Culture.DateTimeFormat.GetMonthName(_Culture.DateTimeFormat.Calendar.GetMonth(date));

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }


        public static string ConvertToMonthName(DateTime? date)
        {
            if (date.HasValue)
                return ConvertToMonthName(date.Value);
            else return string.Empty;
        }

        public static int ConvertToYearName(DateTime date)
        {
            try
            {
                System.Globalization.CultureInfo _Culture = new System.Globalization.CultureInfo("ar-SA", false);
                _Culture.DateTimeFormat.Calendar = new System.Globalization.UmAlQuraCalendar();

                return _Culture.DateTimeFormat.Calendar.GetYear(date);

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int GetMonthNumber(DateTime date)
        {
            try
            {
                System.Globalization.UmAlQuraCalendar _Calendar = new System.Globalization.UmAlQuraCalendar();
                return _Calendar.GetMonth(date);

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int GetMonthNumber(DateTime? date)
        {
            if (date.HasValue)
                return GetMonthNumber(date.Value);
            else return 0;
        }
        public static DateTime SADateNow
        {
            get
            {
                TimeZoneInfo otzi = TimeZoneInfo.FindSystemTimeZoneById("Arabic Standard Time");
                return TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.Utc, otzi);

            }
        }
        public static DateTime FirstDateInMonth(DateTime thedate)
        {



            System.Globalization.UmAlQuraCalendar _Calendar = new System.Globalization.UmAlQuraCalendar();
            DateTime toreturn = _Calendar.ToDateTime(_Calendar.GetYear(thedate), _Calendar.GetMonth(thedate), 1, 0, 0, 0, 0);


            return toreturn;


        }

        public static DateTime LastDateInMonth(DateTime thedate)
        {
            System.Globalization.UmAlQuraCalendar _Calendar = new System.Globalization.UmAlQuraCalendar();

            int year = _Calendar.GetYear(thedate);
            int monuth = _Calendar.GetMonth(thedate);
            DateTime toreturn = _Calendar.ToDateTime(year, monuth, _Calendar.GetDaysInMonth(year, monuth), 0, 0, 0, 0);


            return toreturn;


        }
        public static DateTime AddMonths(DateTime thedate, int by)
        {



            System.Globalization.UmAlQuraCalendar _Calendar = new System.Globalization.UmAlQuraCalendar();

            return _Calendar.AddMonths(thedate, by);

            // DateTime toreturn = _Calendar.ToDateTime(_Calendar.GetYear(thedate), _Calendar.GetMonth(thedate), 1, 0, 0, 0, 0);


            // return toreturn;


        }

        public static int CurrentDayInMonth(DateTime date)
        {
            try
            {
                System.Globalization.UmAlQuraCalendar _Calendar = new System.Globalization.UmAlQuraCalendar();
                return _Calendar.GetDayOfMonth(date);

                //return _Culture.DateTimeFormat.Calendar.GetYear(date).ToString();

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static List<WeekData> MonthRenderCalendar(DateTime basedate)
        {
            List<WeekData> olist = new List<WeekData>();

            DateTime ees = Karasoft.Mvc.Utilities.DateUtility.FirstDateInMonth(basedate);
            DateTime Currentday = Karasoft.Mvc.Utilities.DateUtility.GetFirstDateOfWeek(ees);
            // lalbel1.Text = Karasoft.Web.UI.Utility.DateUtility.ConvertToHijriDate(ees);

            for (int i = 0; i < 5; i++)
            {

                WeekData oweek = new WeekData();
                for (int j = 0; j < 7; j++)
                {

                    CalDayData oday = new CalDayData();
                    oday.Date = Currentday;
                    if (Currentday.Equals(Karasoft.Mvc.Utilities.DateUtility.SADateNow.Date))
                    {
                        oday.IsToday = true;
                        oweek.IsCurrentWeek = true;
                    }
                    switch (j)
                    {
                        case 0:
                            oweek.Sat = oday;
                            break;
                        case 1:
                            oweek.Sun = oday;
                            break;
                        case 2:
                            oweek.Mon = oday;
                            break;
                        case 3:
                            oweek.Tue = oday;
                            break;
                        case 4:
                            oweek.Wed = oday;
                            break;
                        case 5:
                            oweek.Thu = oday;
                            break;
                        case 6:
                            oweek.Fri = oday;
                            break;

                    }

                    Currentday = Currentday.AddDays(1);

                }
                olist.Add(oweek);

            }

            return olist;
        }

        public static WeekData WeekRenderCalendar(DateTime basedate)
        {


            //   DateTime ees = Karasoft.Web.UI.Utility.DateUtility.FirstDateInMonth(basedate);
            DateTime Currentday = Karasoft.Mvc.Utilities.DateUtility.GetFirstDateOfWeek(basedate);
            // lalbel1.Text = Karasoft.Web.UI.Utility.DateUtility.ConvertToHijriDate(ees);



            WeekData oweek = new WeekData();
            for (int j = 0; j < 7; j++)
            {

                CalDayData oday = new CalDayData();
                oday.Date = Currentday;
                if (Currentday.Equals(Karasoft.Mvc.Utilities.DateUtility.SADateNow.Date))
                {
                    oday.IsToday = true;
                    oweek.IsCurrentWeek = true;
                }
                switch (j)
                {
                    case 0:
                        oweek.Sat = oday;
                        break;
                    case 1:
                        oweek.Sun = oday;
                        break;
                    case 2:
                        oweek.Mon = oday;
                        break;
                    case 3:
                        oweek.Tue = oday;
                        break;
                    case 4:
                        oweek.Wed = oday;
                        break;
                    case 5:
                        oweek.Thu = oday;
                        break;
                    case 6:
                        oweek.Fri = oday;
                        break;

                }

                Currentday = Currentday.AddDays(1);



            }

            return oweek;
        }

        public static System.Globalization.UmAlQuraCalendar ArabicCalendar
        {
            get
            {
                return new System.Globalization.UmAlQuraCalendar();
            }
        }
        public static System.Globalization.CultureInfo ArabicCultureInfo
        {
            get
            {
                return new CultureInfo("ar-SA");
            }
        }

        public static TimeComparisonResult CompareTime(DateTime Date1, DateTime Date2)
        {
            return CompareTime(Date1, Date2, false);
        }
        public static TimeComparisonResult CompareTime(DateTime Date1, DateTime Date2, bool IncludeSecond)
        {
            TimeComparisonResult toReturn = TimeComparisonResult.Equal;

            Date2 = new DateTime(Date1.Year, Date1.Month, Date1.Day, Date2.Hour, Date2.Minute, IncludeSecond == true ? Date2.Second : 0);

            if (Date1 > Date2)
                toReturn = TimeComparisonResult.GreaterThan;
            if (Date1 < Date2)
                toReturn = TimeComparisonResult.LessThan;
            return toReturn;
        }


    }

    public struct Age
    {
        public int day;
        public int month;
        public int year;
        public override string ToString()
        {
            return day + "-" + month + "-" + year;
        }


    }

    public class CalDayData
    {
        public DateTime Date { get; set; }
        public bool IsToday { get; set; }
        //
        // Summary:
        //     Gets the string equivalent of the day number for the date represented by
        //     an instance of the System.Web.UI.WebControls.CalendarDay class. This property
        //     is read-only.
        //
        // Returns:
        //     The string equivalent of the day number for the date represented by an instance
        //     of this class.
        public string DayNumberText { get; set; }
        // Summary:
        //     Gets the date represented by an instance of this class. This property is
        //     read-only.
        //
        // Returns:
        //     A System.DateTime object that contains the date represented by an instance
        //     of this class. This allows you to programmatically control the appearance
        //     or behavior of the day, based on this value.
        public string ArabicDate
        {
            get
            {
                return Karasoft.Mvc.Utilities.DateUtility.ConvertToHijriDate(Date);
            }
        }

        public object DayData { get; set; }
    }

    public class WeekData
    {
        public object Tag { get; set; }
        public CalDayData Sat { get; set; }
        public CalDayData Sun { get; set; }
        public CalDayData Mon { get; set; }
        public CalDayData Tue { get; set; }
        public CalDayData Wed { get; set; }
        public CalDayData Thu { get; set; }
        public CalDayData Fri { get; set; }
        public bool IsCurrentWeek { get; set; }
        public int WeekNum
        {
            get
            {
                return Karasoft.Mvc.Utilities.DateUtility.GetWeekNumber(Sat.Date);
            }
        }
    }

    public enum TimeComparisonResult
    {
        Equal,
        GreaterThan,
        LessThan
    }
}
