using SpeedWatch.Interfaces;
using System;

namespace SpeedWatch
{
    public class SpeedWatchTestResult : ISpeedWatchTestResult
    {
        public string SpeedTestName { get; }
        public TimeSpan Elapsed { get; }
        public Exception Exception { get; }

        public SpeedWatchTestResult(string speedTestName, TimeSpan elapsed, Exception exception)
        {
            if (string.IsNullOrEmpty(speedTestName)) throw new ArgumentNullException(nameof(speedTestName), 
                "Speed test name must be provided");
            SpeedTestName = speedTestName;
            Elapsed = elapsed;
            Exception = exception;
        }
    }
}
