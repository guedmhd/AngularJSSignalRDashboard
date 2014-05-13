﻿using System.Diagnostics;
using System.Threading;
using Microsoft.AspNet.SignalR;

namespace SystemMonitorService
{
    public class BackgroundPerformanceDataTimer
    {
        private readonly PerformanceCounter processorCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private Timer taskTimer;
        private IHubContext hub;

        public BackgroundPerformanceDataTimer()
        {
            hub = GlobalHost.ConnectionManager.GetHubContext<PerformanceDataHub>();
            taskTimer = new Timer(OnTimerElapsed, null, 1000, 1000);
        }

        private void OnTimerElapsed(object sender)
        {
            var perfValue = processorCounter.NextValue().ToString("0.0");
            //var r = new Random();
            //var perfValue = r.Next(10, 50).ToString();

            hub.Clients.All.newCpuDataValue(perfValue);
        }

        public void Stop(bool immediate)
        {
            taskTimer.Dispose();
        }
    }
}