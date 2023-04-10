using Common.VieModel;
using System.Globalization;

namespace Common.Utilities
{
    public class CalenderHelper
    {
        public static DateTime ConvertStringData(string date)
        {
            return JalaliToGregorianDateTime(date);
        }
        public static DateTime JalaliToGregorianDateTime(string dateTime)
        {
            string[] strs = dateTime.Split('/');
            PersianCalendar pc = new PersianCalendar();

            if (strs.Length != 3)       //  when we don't select dateTimePickerManager, it returns 0:0 
            {
                return DateTime.MinValue;
            }
            else
            {
                if (strs[0].Length == 4)
                {
                    string[] parts = strs[2].Split(' ');

                    if (parts.Length > 1)
                    {
                        string[] timeParts = parts[1].Split(':');
                        string[] secondParts = timeParts[2].Split('.');
                        return pc.ToDateTime(Convert.ToInt32(strs[0]), Convert.ToInt32(strs[1]), Convert.ToInt32(parts[0]), Convert.ToInt32(timeParts[0]), Convert.ToInt32(timeParts[1])
                            , Convert.ToInt32(secondParts[0]), Convert.ToInt32(secondParts[1]));
                    }
                    else
                    {
                        return pc.ToDateTime(Convert.ToInt32(strs[0]), Convert.ToInt32(strs[1]), Convert.ToInt32(strs[2]), 0, 0, 0, 0);
                    }
                }
                else
                {
                    string[] parts = strs[2].Split(' ');

                    if (parts.Length > 1)
                    {
                        string[] timeParts = parts[1].Split(':');
                        string[] secondParts = timeParts[2].Split('.');
                        return pc.ToDateTime(Convert.ToInt32(parts[0]), Convert.ToInt32(strs[0]), Convert.ToInt32(strs[1]), 0, 0, 0, 0);
                    }
                    else
                    {
                        return pc.ToDateTime(Convert.ToInt32(strs[2]), Convert.ToInt32(strs[1]), Convert.ToInt32(strs[0]), 0, 0, 0, 0);
                    }
                }
            }
        }
        public static IEnumerable<DateOfIncome> GetDateBetweenTwoDates(DateTime start, DateTime end)
        {
            List<DateOfIncome> dateOfIncomes = new List<DateOfIncome>();
            for (var dateTime = start; dateTime <= end; dateTime = dateTime.AddDays(1))
            {
                dateOfIncomes.Add(new DateOfIncome { Date = dateTime });
            }
            return dateOfIncomes;
        }
        public static string MiladiToShamsi(DateTime date)
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                var year = pc.GetYear(date);
                var month = pc.GetMonth(date);
                var day = pc.GetDayOfMonth(date);
                return
                    $"{year.ToString().PadLeft(4, '0')}/{month.ToString().PadLeft(2, '0')}/{day.ToString().PadLeft(2, '0')}";
            }
            catch (Exception e)
            {
                throw new Exception($"Sended date: {date} {e.Message}");
            }
        }
    }
}
