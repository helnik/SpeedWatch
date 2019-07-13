using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpeedWatchTests
{
    internal static class SampleActions
    {
        internal static void WaitTwoSeconds()
        {
            Thread.Sleep(2000);
        }

        internal static async Task WaitTwoSecondsAsync()
        {
            await Task.Delay(2000);
        }

        internal static void ThrowOnEven(int number)
        {
            if (number % 2 == 0) throw new ArithmeticException("Even Number!");
        }
    }
}
