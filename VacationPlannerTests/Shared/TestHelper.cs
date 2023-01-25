using System;
using System.Collections.Generic;
using HR.Vacations.Code;
using HR.Vacations.Code.Interfaces;
using static HR.Vacations.Code.VacationEarningPeriod;

namespace HR.Vacations.Test.Shared
{
    public static  class TestHelper
    {
        public static IVacationPeriodCalculator BuildFinnishVacationCalculator()
        {
            var period = BuildFinnishVacationPeriod2021_2022();

            return new FinnishVacationCalculator(period);
        }

        public static VacationEarningPeriod BuildFinnishVacationPeriod2021_2022()
        {
            return BuildVacationEarningPeriod(
                VacationEarningPeriodType.Finland, 
                new DateTime(2021, 4, 1), 
                new DateTime(2022, 3, 31), 
                UseFinnishPublicHolidaysFor2021_2022(), 
                50
            );
        }

        public static VacationEarningPeriod BuildVacationEarningPeriod(
            VacationEarningPeriodType type, 
            DateTime start, 
            DateTime end, 
            List<DateTime> publicHolidays, 
            int vacationPeriodMaxDays)
        {
            return new VacationEarningPeriod(type, start, end, publicHolidays, vacationPeriodMaxDays);
        }

        public static List<DateTime> UseFinnishPublicHolidaysFor2021_2022()
        {
            return new List<DateTime>()
            {
                new DateTime(2021, 1, 1),
                new DateTime(2021, 1, 6),
                new DateTime(2021, 4, 2),
                new DateTime(2021, 4, 5),
                new DateTime(2021, 5, 13),
                new DateTime(2021, 6, 25),
                new DateTime(2021, 12, 6),
                new DateTime(2021, 12, 24),
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 6),
                new DateTime(2022, 4, 15),
                new DateTime(2022, 4, 18),
                new DateTime(2022, 5, 1),
                new DateTime(2022, 5, 26),
                new DateTime(2022, 6, 5),
                new DateTime(2022, 6, 24),
                new DateTime(2022, 6, 25),
                new DateTime(2022, 12, 6),
                new DateTime(2022, 12, 24),
                new DateTime(2022, 12, 25),
                new DateTime(2022, 12, 26)
            };
        }
    }
}
