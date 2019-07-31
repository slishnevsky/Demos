using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace TopshelfDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<IntegrationService>(s =>
                {
                    s.ConstructUsing(() => new IntegrationService());
                    s.WhenStarted(y => y.Start());
                    s.WhenStopped(y => y.Stop());
                });

                x.EnableServiceRecovery(r =>
                {
                    r.RestartService(1);
                    r.RestartService(1);
                    r.OnCrashOnly();
                    r.SetResetPeriod(1);
                });

                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.SetDescription("TopshelfDemo Description");
                x.SetServiceName("TopshelfDemo ServiceName");
                x.SetDisplayName("TopshelfDemo DisplayName");
            });
        }
    }
}
