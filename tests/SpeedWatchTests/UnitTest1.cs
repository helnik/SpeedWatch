using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpeedWatch;

namespace SpeedWatchTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestWithNoExceptionsResult()
        {
            var test = new SpeedWatchTest("Simple Test", SampleActions.WaitTwoSeconds,5, "");
            var result = test.GetResults();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 5);
            foreach (var r in result)
            {
                Assert.IsTrue(r.Exception == null);
                Assert.IsTrue(r.Elapsed.Seconds == 2);
            }
        }

        [TestMethod]
        public void TestWithNoExceptionsAsyncResult()
        {
            var task1 = SampleActions.WaitTwoSecondsAsync();
            var task2 = SampleActions.WaitTwoSecondsAsync();

            var test = new SpeedWatchTest("Simple Test", () => Task.WaitAll(task1, task2),
                 5, "");
            var result = test.GetResults();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 5);
            foreach (var r in result)
            {
                Assert.IsTrue(r.Exception == null);
            }
        }

        [TestMethod]
        public void TestWithNoExceptionsSummary()
        {
            var test = new SpeedWatchTest("Simple Test", SampleActions.WaitTwoSeconds, 2, "");
            var result = test.GetSummary();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.TestResults.Count == 2);
            Assert.IsTrue(result.PassedTests == 2);
            Assert.IsTrue(Math.Round(result.PassedTestsAverageTime, MidpointRounding.ToEven) == 2001);
            Assert.IsTrue(result.FailedTests == 0);
            Assert.IsTrue(result.FailedTestsAverageTime == 0);
            Trace.WriteLine(result.ToString());
            Assert.IsFalse(string.IsNullOrEmpty(result.ToString()));
        }

        [TestMethod]
        public void TestWithExceptions()
        {
            var test = new SpeedWatchTest("Simple Test", 
                () => SampleActions.ThrowOnEven(2), 2, "");
            var result = test.GetSummary();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.TestResults.Count == 2);
            Assert.IsTrue(result.PassedTests == 0);
            Assert.IsTrue(result.FailedTests == 2);
            Trace.WriteLine(test.GetSummary().ToString());
        }

        [TestMethod]
        public void QuickMeasure()
        {
            Action action = SampleActions.WaitTwoSeconds;
            var elapsed = action.Measure();
            Assert.IsTrue(elapsed.Seconds == 2);
            action = () => SampleActions.ThrowOnEven(2);
            elapsed = action.Measure();
            Assert.IsTrue(elapsed.Milliseconds > 0);
        }

        [TestMethod]
        public void QuickAverage()
        {
            Action action = SampleActions.WaitTwoSeconds;
            var average = action.MeasureAverage(2);
            Assert.IsTrue(Math.Round(average) <= 2001);
        }

    }
}
