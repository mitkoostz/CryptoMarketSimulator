using BombaySapphireCds.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

    namespace BombaySapphireCds.App_Start
    {
        public static class ChartsWorkerConfig
        {
            private static ChartsWorker m_job;

            public static void Start()
            {
                m_job = new ChartsWorker(TimeSpan.FromMinutes(5));
            }

            public static void Shutdown()
            {
                m_job.Dispose();
            }
        }
    }
