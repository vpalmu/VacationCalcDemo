
namespace HR.Vacations.Code.Interfaces
{   public interface IVacationPeriodCalculator
    {
        int CalculateConsumedDays(DateTime startDate, DateTime endDate);
    }
}