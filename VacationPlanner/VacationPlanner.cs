using HR.Vacations.Code.Interfaces;
using static HR.Vacations.Code.VacationEarningPeriod;

namespace HR.Vacations.Code
{
    public class VacationPlanner
    {
        private VacationEarningPeriod _vacationEarningPeriod;
        private IVacationPeriodCalculator _vacationPeriodCalculator;


        public VacationPlanner(VacationEarningPeriod vacationEarningPeriod)
        {
            if (vacationEarningPeriod == null)
                throw new ArgumentNullException(nameof(vacationEarningPeriod));

            _vacationEarningPeriod = vacationEarningPeriod;
            _vacationPeriodCalculator = InitializeCalculator(_vacationEarningPeriod.Type);
        }

        private IVacationPeriodCalculator InitializeCalculator(VacationEarningPeriodType type)
        {
            return type switch
            {
                VacationEarningPeriodType.Finland => new FinnishVacationCalculator(_vacationEarningPeriod),
                VacationEarningPeriodType.Sweden => throw new NotImplementedException("Sweden not supported"),
                _ => throw new NotImplementedException("null"),
            };
        }

        public int CalculateVacationDaysSpent(DateTime startDate, DateTime endDate)
        {
            return _vacationPeriodCalculator.CalculateConsumedDays(startDate, endDate);
        }
    }
}
