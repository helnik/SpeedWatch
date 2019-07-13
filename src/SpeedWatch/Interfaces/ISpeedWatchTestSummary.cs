using System.Collections.Generic;

namespace SpeedWatch.Interfaces
{
    public interface ISpeedWatchTestSummary
    {
        string SpeedTestName { get; }
        string SpeedTestDescription { get; }
        List<ISpeedWatchTestResult> TestResults { get; }
        int PassedTests { get; }
        int FailedTests { get; }
        double AverageTime { get; }
        double PassedTestsAverageTime { get; }
        double FailedTestsAverageTime { get; }
    }
}
