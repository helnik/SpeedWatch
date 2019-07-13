using SpeedWatch.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpeedWatch
{
    public class SpeedWatchTest : ISpeedWatchTest
    {
        public string SpeedTestName { get; }
        public string SpeedTestDescription { get; }
        public Action Action { get; }
        public int NoOfExecutions { get; }

        public SpeedWatchTest(string name, Action action, int noOfExecutions, string speedTestDescription)
        {
              if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name), "Test name must be provided");
              SpeedTestName = name;
              Action = action ?? throw new ArgumentNullException(nameof(action), "Test action be provided");
              if (noOfExecutions < 1) throw new ArgumentOutOfRangeException(nameof(noOfExecutions), "Execution runs must be equal or greater to 1");
              NoOfExecutions = noOfExecutions;
              SpeedTestDescription = speedTestDescription;
        }

        public List<ISpeedWatchTestResult> GetResults()
        {
            return RunTestNTimes();
        }

        public ISpeedWatchTestSummary GetSummary()
        {
            var result = RunTestNTimes();
            return new SpeedWatchTestSummary(result, SpeedTestName, SpeedTestDescription);
        }

        private List<ISpeedWatchTestResult> RunTestNTimes()
        {
            var result = new List<ISpeedWatchTestResult>();
            for (int i = 0; i < NoOfExecutions; i++)
                result.Add(Execute());
            return result;
        }

        private ISpeedWatchTestResult Execute()
        {
            GC.Collect();
            Exception ex = null;
            var sw = Stopwatch.StartNew();
            try
            {
                Action();
            }
            catch (Exception e)
            {
                ex = e;
            }
            sw.Stop();

            return new SpeedWatchTestResult(SpeedTestName, sw.Elapsed, ex);
        }

        

        public override string ToString()
        {
            return SpeedTestName;
        }
    }
}
