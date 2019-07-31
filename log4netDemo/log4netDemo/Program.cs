using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

[assembly: log4net.Config.XmlConfigurator]

namespace log4netDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Default configuration, can be replaced by [assembly:log4net.Config.XmlConfigurator] above
            //log4net.Config.BasicConfigurator.Configure();

            var logger = LogManager.GetLogger(typeof(Program));

            logger.Debug("DEBUG");
            logger.Info("INFO");
            logger.Warn("WARN");
            logger.Error("ERROR");
            logger.Fatal("FATAL");

        }
    }
}
