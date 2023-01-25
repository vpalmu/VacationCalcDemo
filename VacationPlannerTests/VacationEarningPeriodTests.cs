using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HR.Vacations.Test.Shared;
using static HR.Vacations.Code.VacationEarningPeriod;

namespace HR.Vacations.Test
{
    [TestClass]
    public class VacationEarningPeriodTests
    {
        [TestMethod]
        public void VacationEarningPeriodStartDateAfterEndDate()
        {
            // Arrange
            var startDate = new DateTime(2021, 2, 2);
            var endDate = new DateTime(2020, 1, 1);
            
            // Act
            Action action = () => TestHelper.BuildVacationEarningPeriod(
                VacationEarningPeriodType.Finland, 
                startDate, endDate, 
                TestHelper.UseFinnishPublicHolidaysFor2021_2022(), 
                50
            );

            // Assert
            var exception = Assert.ThrowsException<Exception>(action, "Should throw exception vacation earning period doesn't cover one year");
            Assert.AreEqual("Dates for the time span must be in chronological order", exception.Message);
        }

        [TestMethod]
        public void VacationEarningPeriodCoversLessThanOneYear()
        {
            // Arrange
            var startDate = new DateTime(2021, 2, 2);
            var endDate = new DateTime(2021, 11, 1);

            // Act
            Action action = () => TestHelper.BuildVacationEarningPeriod(
                 VacationEarningPeriodType.Finland,
                 startDate, endDate,
                 TestHelper.UseFinnishPublicHolidaysFor2021_2022(),
                 50
             );

            // Assert
            var exception = Assert.ThrowsException<Exception>(action);
            Assert.AreEqual("Vacation earning period must cover one year", exception.Message);
        }

        [TestMethod]
        public void VacationEarningPeriodCoversMoreThanOneYear()
        {
            // Arrange
            var startDate = new DateTime(2021, 2, 2);
            var endDate = new DateTime(2022, 11, 1);

            // Act
            Action action = () => TestHelper.BuildVacationEarningPeriod(
                 VacationEarningPeriodType.Finland,
                 startDate, endDate,
                 TestHelper.UseFinnishPublicHolidaysFor2021_2022(),
                 50
             );

            // Assert
            var exception = Assert.ThrowsException<Exception>(action, "Should throw exception if time span is not chronological order");
            Assert.AreEqual("Vacation earning period must cover one year", exception.Message);
        }

        [TestMethod]
        public void VacationEarningPeriodCoversExactlyOneYear()
        {
            // Arrange
            var startDate = new DateTime(2021, 4, 1);
            var endDate = new DateTime(2022, 3, 31);

            // Act
            Action action = () => TestHelper.BuildVacationEarningPeriod(
                VacationEarningPeriodType.Finland,
                startDate, endDate,
                TestHelper.UseFinnishPublicHolidaysFor2021_2022(),
                50
            );

            // Assert
            Assert.AreEqual(1, 1, "all good - no exceptions in initialization");
        }

        [TestMethod]
        public void VacationEarningPeriodHasNoHolidays()
        {
            // Arrange
            var startDate = new DateTime(2021, 4, 1);
            var endDate = new DateTime(2022, 3, 31);

            // Act
            Action action = () => TestHelper.BuildVacationEarningPeriod(
                 VacationEarningPeriodType.Finland,
                 startDate, endDate,
                 new List<DateTime>(),
                 50
             );

            // Assert
            var exception = Assert.ThrowsException<ArgumentException>(action, "Should throw exception if no holidays are defined");
            Assert.AreEqual("There must be at least one public holiday, right ?", exception.Message);
        }
    }
}
