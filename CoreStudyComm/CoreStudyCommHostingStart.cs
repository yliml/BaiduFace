
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

//在这里添加的是把HostingStartup标签打上，以便程序启动的时候找到对应的类
[assembly:HostingStartup(typeof(CoreStudyComm.CoreStudyCommHostingStart))]
namespace CoreStudyComm
{
    public class CoreStudyCommHostingStart : IHostingStartup
    {
        /// <summary>
        /// 这个是IHostingStartup的方法，输入一个IwebHostBuilder
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(IWebHostBuilder builder)
        {
            //这里是注册Service的
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IPeople>(new People("CoreStudyCommHostingStart"));
                services.AddSingleton<IStartupFilter,StartUpFilterA>();
                services.AddSingleton<IStartupFilter, StartUpFilterB>();
            });

            ////这里是注册配置文件的
            builder.ConfigureAppConfiguration((context, configBuild) =>
            {

            });

            //不要在这里调用configure方法，这里注册中间件的话会把StartUp整个文件无效掉
            builder.Configure(app =>
            {
                app.Run(context =>
                {
                    Console.WriteLine("其他库配置Middleware");
                    context.Response.WriteAsync("111");
                    return Task.CompletedTask;
                });
            });     
        }
    }
}
