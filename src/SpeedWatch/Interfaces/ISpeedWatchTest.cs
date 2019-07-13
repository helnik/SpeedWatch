using System;
using System.Collections.Generic;

namespace SpeedWatch.Interfaces
{
    public interface ISpeedWatchTest
    {
        /// <summary>
        /// Name
        /// </summary>
        string SpeedTestName { get; }

        string SpeedTestDescription { get; }

        /// <summary>
        /// The action to put under watch
        /// </summary>
        Action Action { get; }

        /// <summary>
        /// Number of <see cref="Action"/> test executions
        /// </summary>
        int NoOfExecutions { get; }

        /// <summary>
        /// The outcome of the test
        /// </summary>
        List<ISpeedWatchTestResult> GetResults();

        /// <summary>
        /// Get a test summary
        /// </summary>
        ISpeedWatchTestSummary GetSummary();
    }
}
