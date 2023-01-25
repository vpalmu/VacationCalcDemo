
namespace HR.Vacations.Code.Extensions
{
    public static class DateTimeExtensions
    {
        public static List<DateTime> RemoveSundays(this List<DateTime> vacationPeriod)
        {
            return vacationPeriod.Where(x => x.DayOfWeek != DayOfWeek.Sunday).ToList();
        }

        public static List<DateTime> RemovePublicHolidays(this List<DateTime> periodWithoutSundays, List<DateTime> publicHolidays)
        {   
            return periodWithoutSundays.Where(date => !publicHolidays.Contains(date)).ToList();
        }

        public static List<DateTime> GetAllDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                allDates.Add(date.Date);
            }

            return allDates;
        }
    }
}
