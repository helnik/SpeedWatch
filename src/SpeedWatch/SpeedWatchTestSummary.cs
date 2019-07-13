using SpeedWatch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpeedWatch
{
    public class SpeedWatchTestSummary : ISpeedWatchTestSummary
    {
        public string SpeedTestName { get; }

        public string SpeedTestDescription { get; }

        public List<ISpeedWatchTestResult> TestResults { get; }

        public int PassedTests { get; }

        public int FailedTests { get; }

        public double PassedTestsAverageTime { get; }

        public double FailedTestsAverageTime { get; }

        public double AverageTime { get; }

        public SpeedWatchTestSummary(List<ISpeedWatchTestResult> testResults, string testName, string testDescription)
        {
                if(testResults == null)
                    throw new ArgumentNullException(nameof(testResults), "A list with test results must be provided");

                if(!testResults.Any())
                    throw new ArgumentOutOfRangeException(nameof(testResults), "The test results list must contain at least 1 element");

                if (string.IsNullOrEmpty(testName))
                    throw new ArgumentNullException(nameof(testName), "The test Name must be provided");

                SpeedTestName = testName;
                SpeedTestDescription = testDescription;
                TestResults = testResults;
                var passedTests = testResults.Where(tr => tr.Exception == null).ToList();
                var failedTests = testResults.Where(tr => tr.Exception != null).ToList();
                PassedTests = passedTests.Count;
                FailedTests = failedTests.Count;
                AverageTime = testResults.Average(t => t.Elapsed.TotalMilliseconds);
                PassedTestsAverageTime = passedTests.Count > 0 
                    ? passedTests.Average(pt => pt.Elapsed.TotalMilliseconds) : 0;
                FailedTestsAverageTime = failedTests.Count > 0    
                ? failedTests.Average(ft => ft.Elapsed.TotalMilliseconds) : 0;
        }

        public override string ToString()
        {
            string description = string.IsNullOrEmpty(SpeedTestDescription)
                ? " "
                : $" with description: {SpeedTestDescription} ";

            return $@"Test named: {SpeedTestName}{description}run {TestResults.Count} times with average execution time: {AverageTime}ms.
                      Results Summary:
                      Passed Tests: {PassedTests} - Average Time: {PassedTestsAverageTime}ms
                      Failed Tests: {FailedTests} - Average Time {FailedTestsAverageTime}ms";
        }
    }
}

