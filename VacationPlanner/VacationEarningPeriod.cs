
namespace HR.Vacations.Code
{
    public class VacationEarningPeriod
    {
        public DateTime PeriodStart { get; }
        public DateTime PeriodEnd { get; }
        public List<DateTime> PublicHolidaysInPeriod { get; }
        public VacationEarningPeriodType Type  { get; }
        public int VacationPeriodMaxDays { get; }

        public enum VacationEarningPeriodType
        {
            Finland,
            Sweden
        }

        public VacationEarningPeriod(VacationEarningPeriodType type, DateTime startDate, DateTime endDate, List<DateTime> publicHolidays, int vacationPeriodMaxDays)
        {
            if (startDate > endDate)
                throw new Exception("Dates for the time span must be in chronological order");

            if (startDate.AddYears(1).AddDays(-1) != endDate)
                throw new Exception("Vacation earning period must cover one year");

            PeriodStart = startDate;
            PeriodEnd = endDate;

            if (publicHolidays == null)
                throw new ArgumentNullException(nameof(publicHolidays));

            if (publicHolidays.Count == 0)
                throw new ArgumentException("There must be at least one public holiday, right ?");

            PublicHolidaysInPeriod = publicHolidays;
            Type = type;
            VacationPeriodMaxDays = vacationPeriodMaxDays;
        }
    }
}
