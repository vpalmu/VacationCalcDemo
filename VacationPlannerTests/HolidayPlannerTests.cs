using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HR.Vacations.Test.Shared;
using static HR.Vacations.Code.VacationEarningPeriod;
using HR.Vacations.Code;

namespace HR.Vacations.Test
{
    [TestClass]
    public class HolidayPlannerTest
    {
        [TestMethod]
        public void VacationEarningPeriodNotProvided()
        {
            // Arrange & Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Action action = () => new VacationPlanner(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert
            var exception = Assert.ThrowsException<ArgumentNullException>(action, "Should throw exception if vacation period is not set");
            Assert.AreEqual("Value cannot be null. (Parameter 'vacationEarningPeriod')", exception.Message);
        }

        [TestMethod]
        public void HolidayPlannerDoesNotSupportSweden()
        {
            // Arrange
            var periodForSweden = TestHelper.BuildVacationEarningPeriod(
                VacationEarningPeriodType.Sweden,
                new DateTime(2021, 1, 1), new DateTime(2021, 12, 31), 
                new List<DateTime>() { new DateTime(2021,12,25) }, 
                30);

            // Act
            Action action = () => new VacationPlanner(periodForSweden);

            // Assert
            var exception = Assert.ThrowsException<NotImplementedException>(action, "Should throw exception if vacation calculator is not set");
            Assert.AreEqual("Sweden not supported", exception.Message);
        }

        [TestMethod]
        public void HolidayPlannerSupportsFinland()
        {
            // Arrange
            var periodForFinland = TestHelper.BuildFinnishVacationPeriod2021_2022();

            // Act
            var planner = new VacationPlanner(periodForFinland);

            // Assert
            Assert.AreEqual(1, 1, "no errors until here - pass");
        }
    }
}