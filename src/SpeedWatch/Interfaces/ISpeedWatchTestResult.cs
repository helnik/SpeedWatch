using System;

namespace SpeedWatch.Interfaces
{
    public interface ISpeedWatchTestResult
    {
        string SpeedTestName { get; } 
        TimeSpan Elapsed { get; }
        Exception Exception { get; }
    }
}
