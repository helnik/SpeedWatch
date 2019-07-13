using System;
using System.Diagnostics;

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
    }
}
