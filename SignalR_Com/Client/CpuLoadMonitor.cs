using System.Diagnostics;

namespace Client
{
    public class CpuLoadMonitor
    {
        private readonly Queue<float> _cpuLoadQueue = new Queue<float>();
        private readonly PerformanceCounter _cpuCounter = new("Processor", "% Processor Time", "_Total");

        public float GetAvgCpuLoad()
        {
            var currentCpuLoad = _cpuCounter.NextValue();
            _cpuLoadQueue.Enqueue(currentCpuLoad);

            if (_cpuLoadQueue.Count > 60)
            {
                _cpuLoadQueue.Dequeue();
            }

            if (_cpuLoadQueue.Count < 60)
            {
                return currentCpuLoad;
            }

            return CalculateAverageLoad();
        }

        private float CalculateAverageLoad()
        {
            float total = 0;
            foreach (var load in _cpuLoadQueue)
            {
                total += load;
            }

            return total / _cpuLoadQueue.Count;
        }
    }
}
