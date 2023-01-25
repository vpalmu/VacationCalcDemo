using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using HR.Vacations.Code;
using HR.Vacations.Test.Shared;

namespace HR.Vacations.Test
{
    [TestClass]
    public class FinnishVacationCalculatorTests
    {
        [TestMethod]
        public void VacationEarningPeriodNotInformed()
        {
            // Arrange
            var startDate = new DateTime(2021, 2, 2);
            var endDate = new DateTime(2020, 1, 1);

            // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Action action = () => new FinnishVacationCalculator(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert
            var exception = Assert.ThrowsException<ArgumentNullException>(action, "Should throw exception if vacation period is not set");
            Assert.AreEqual("Value cannot be null. (Parameter 'vacationEarningPeriod')", exception.Message);
        }

        [TestMethod]
        public void VacationStartDateAfterEndDate()
        {
            // Arrange
            var startDate = new DateTime(2021, 6, 24);
            var endDate = new DateTime(2020, 7, 24);
            var calculator = new FinnishVacationCalculator(TestHelper.BuildFinnishVacationPeriod2021_2022());

            // Act
            Action action = () => calculator.CalculateConsumedDays(startDate, endDate);

            // Assert
            var exception = Assert.ThrowsException<Exception>(action, "Should throw exception if time span is not chronological order");
            Assert.AreEqual("Dates for the time span must be in chronological order", exception.Message);
        }

        [TestMethod]
        public void MaxLengthOfVacationIsMoreThan50Days()
        {
            // Arrange
            var startDate = new DateTime(2021, 6, 24);
            var endDate = new DateTime(2021, 8, 24);
            var calculator = new FinnishVacationCalculator(TestHelper.BuildFinnishVacationPeriod2021_2022());

            // Act
            Action action = () => calculator.CalculateConsumedDays(startDate, endDate);

            // Assert
            var exception = Assert.ThrowsException<Exception>(action, "Should throw exception if maximum length of the vacation is more than 50 days");
            Assert.AreEqual("The maximum length of the vacation is 50 days", exception.Message);
        }

        [TestMethod]
        public void VacationsStartsBeforeVacationEarningPeriod()
        {
            // Arrange
            var startDate = new DateTime(2020, 6, 24);
            var endDate = new DateTime(2020, 7, 24);
            var calculator = new FinnishVacationCalculator(TestHelper.BuildFinnishVacationPeriod2021_2022());

            // Act
            Action action = () => calculator.CalculateConsumedDays(startDate, endDate);

            // Assert
            var exception = Assert.ThrowsException<Exception>(action, "Should throw exception if vacation is not inside vacation period");
            Assert.AreEqual("Whole time span has to be within the same vacation period: 4/1/2021 12:00:00 AM - 3/31/2022 12:00:00 AM", exception.Message);
        }

        [TestMethod]
        public void VacationsEndsAfterVacationEarningPeriod()
        {
            // Arrange
            var startDate = new DateTime(2022, 6, 24);
            var endDate = new DateTime(2022, 7, 24);
            var calculator = new FinnishVacationCalculator(TestHelper.BuildFinnishVacationPeriod2021_2022());

            // Act
            Action action = () => calculator.CalculateConsumedDays(startDate, endDate);

            // Assert
            var exception = Assert.ThrowsException<Exception>(action, "Should throw exception if vacation is not inside vacation period");
            Assert.AreEqual("Whole time span has to be within the same vacation period: 4/1/2021 12:00:00 AM - 3/31/2022 12:00:00 AM", exception.Message);
        }

        [TestMethod]
        [Description("Does not make much sense, but techically possible")]
        public void OneDayVacationOnNormalMonday()
        {
            // Arrange
            var startDate = new DateTime(2022, 3, 1);
            var endDate = new DateTime(2022, 3, 1);
            var calculator = new FinnishVacationCalculator(TestHelper.BuildFinnishVacationPeriod2021_2022());
            
            // Act
            var days = calculator.CalculateConsumedDays(startDate, endDate);

            // Assert
            Assert.AreEqual(1, days);
        }

        [TestMethod]
        [Description("Does not make much sense, but techically possible")]
        public void OneDayVacationOnSunday()
        {
            // Arrange
            var startDate = new DateTime(2022, 3, 6);
            var endDate = new DateTime(2022, 3, 6);
            var calculator = new FinnishVacationCalculator(TestHelper.BuildFinnishVacationPeriod2021_2022());

            // Act
            var days = calculator.CalculateConsumedDays(startDate, endDate);

            // Assert
            Assert.AreEqual(0, days);
        }

        [TestMethod]
        [Description("March 2022 has no public holidays :(")]
        public void WholeMonthVacationOnMarch2022()
        {
            // Arrange
            var startDate = new DateTime(2022, 3, 1);
            var endDate = new DateTime(2022, 3, 31);
            var calculator = new FinnishVacationCalculator(TestHelper.BuildFinnishVacationPeriod2021_2022());

            // Act
            var days = calculator.CalculateConsumedDays(startDate, endDate);

            // Assert
            Assert.AreEqual(27, days);
        }

        [TestMethod]
        [Description("January 2022 has some public holidays")]
        public void WholeMonthVacationOnJanuary2022()
        {
            // Arrange
            var startDate = new DateTime(2022, 1, 1);
            var endDate = new DateTime(2022, 1, 31);
            var calculator = new FinnishVacationCalculator(TestHelper.BuildFinnishVacationPeriod2021_2022());

            // Act
            var days = calculator.CalculateConsumedDays(startDate, endDate);

            // Assert
            Assert.AreEqual(24, days);
        }

        [TestMethod]
        [Description("Midsummer is the most popular time to start vacationing in Finland")]
        public void MidsummerVacation2021()
        {
            // Arrange
            var startDate = new DateTime(2021, 6, 24);
            var endDate = new DateTime(2021, 7, 18);
            var calculator = new FinnishVacationCalculator(TestHelper.BuildFinnishVacationPeriod2021_2022());

            // Act
            var days = calculator.CalculateConsumedDays(startDate, endDate);

            // Assert
            Assert.AreEqual(20, days);
        }

        [TestMethod]
        [Description("Christmas (with New year) is very popular time to have a short break in Finland")]
        public void ChristmasVacation2021()
        {
            // Arrange
            var startDate = new DateTime(2021, 12, 24);
            var endDate = new DateTime(2022, 1, 7);
            var calculator = new FinnishVacationCalculator(TestHelper.BuildFinnishVacationPeriod2021_2022());

            // Act
            var days = calculator.CalculateConsumedDays(startDate, endDate);

            // Assert
            Assert.AreEqual(10, days);
        }
    }
}
