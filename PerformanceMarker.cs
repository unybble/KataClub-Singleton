using System;
namespace Singleton
{
    public sealed class PerformanceMarker
    {
        private double total;
        private double count;

        private PerformanceMarker()
        {
            total = 0;
            count = 0;
        }

        public double GetAvg()
        {
            return total / count;
        }

        public void RecordEvent(double seconds)
        {
            total += seconds;
            count++;
        }

        public static PerformanceMarker Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly PerformanceMarker instance = new PerformanceMarker();
        }
    }


}