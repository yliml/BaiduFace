using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace CoreStudy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        //public static IWebHost BuildWebHost(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>()
        //        .Build();

        //设置用的环境
        public static IWebHost BuildWebHost(string[] args) =>
    WebHost.CreateDefaultBuilder(args).UseSetting(WebHostDefaults.HostingStartupAssembliesKey, "CoreStudyComm").UseEnvironment("Development").UseUrls()
        .UseStartup("CoreStudy").UseShutdownTimeout()
        .Build();
    }
}
