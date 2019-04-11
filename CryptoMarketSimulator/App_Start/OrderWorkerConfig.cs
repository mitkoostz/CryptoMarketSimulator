using System;
using BombaySapphireCds.Jobs;


//[assembly: WebActivator.PostApplicationStartMethod(typeof(BombaySapphireCds.App_Start.PodMonitorConfig), "Start")]
//[assembly: WebActivator.ApplicationShutdownMethod(typeof(BombaySapphireCds.App_Start.PodMonitorConfig), "Shutdown")]

namespace BombaySapphireCds.App_Start
{
    public static class OrderWorkerConfig
    {
        private static OrderWorker m_job;

        public static void Start()
        {
            m_job = new OrderWorker(TimeSpan.FromSeconds(4));
        }

        public static void Shutdown()
        {
            m_job.Dispose();
        }
    }
}