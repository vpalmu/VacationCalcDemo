using HR.Vacations.Code.Extensions;
using HR.Vacations.Code.Interfaces;

namespace HR.Vacations.Code
{
    public class FinnishVacationCalculator : IVacationPeriodCalculator
    {
        private VacationEarningPeriod _vacationEarningPeriod;

        public FinnishVacationCalculator(VacationEarningPeriod vacationEarningPeriod)
        {
            if (vacationEarningPeriod == null)
                throw new ArgumentNullException(nameof(vacationEarningPeriod));

            _vacationEarningPeriod = vacationEarningPeriod;
        }

        public int CalculateConsumedDays(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new Exception("Dates for the time span must be in chronological order");

            if (endDate.Subtract(startDate).Days > _vacationEarningPeriod.VacationPeriodMaxDays)
                throw new Exception($"The maximum length of the vacation is {_vacationEarningPeriod.VacationPeriodMaxDays} days");

            if (startDate < _vacationEarningPeriod.PeriodStart || startDate > _vacationEarningPeriod.PeriodEnd)
                throw new Exception($"Whole time span has to be within the same vacation period: {_vacationEarningPeriod.PeriodStart} - {_vacationEarningPeriod.PeriodEnd}");

            if (endDate < _vacationEarningPeriod.PeriodStart || endDate > _vacationEarningPeriod.PeriodEnd)
                throw new Exception($"Whole time span has to be within the same vacation period: {_vacationEarningPeriod.PeriodStart} - {_vacationEarningPeriod.PeriodEnd}");


            var consumedVacationDays = DateTimeExtensions.GetAllDatesBetween(startDate, endDate)
                                                         .RemoveSundays()
                                                         .RemovePublicHolidays(_vacationEarningPeriod.PublicHolidaysInPeriod);

            return consumedVacationDays.Count();
        }
    }
}
