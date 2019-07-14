using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SpeedWatch
{
    public static class Extensions
    {
        public static TimeSpan Measure(this Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action),"An action to measure must be provided");
            GC.Collect();
            var sw = Stopwatch.StartNew();
            try
            {
                action();
            }
            catch (Exception)
            {
                // don't care
            }
            sw.Stop();
            return sw.Elapsed;
        }

        public static double MeasureAverage(this Action action, int executions)
        {
            if (action == null) throw new ArgumentNullException(nameof(action), "An action to measure must be provided");
            if (executions < 1) throw new ArgumentOutOfRangeException(nameof(executions), "No of executions must be greater than 1");

            var results = new List<double>();
            for (int i = 0; i < executions; i++)
                results.Add(action.Measure().TotalMilliseconds);

            return results.Average();
        }
    }
}
