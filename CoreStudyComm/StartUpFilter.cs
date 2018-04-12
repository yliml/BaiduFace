using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace CoreStudyComm
{
    public class StartUpFilterA : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            Console.WriteLine("过滤器A");
            return context => {

                Console.WriteLine("进入过滤器A");
                //注册了一个MiddleA
                context.Use(next1 => {
                    Console.WriteLine("Used Middle A!!!");
                    return async (context1) => {
                        Console.WriteLine("Beging Middle A!!");
                        await next1(context1);
                        Console.WriteLine("Ending Middle A!!");
                    };
                });
                next(context);
                Console.WriteLine("结束过滤器A");
            };
        }
    }

    public class StartUpFilterB : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            Console.WriteLine("过滤器B");
            return context => {

                Console.WriteLine("进入过滤器B");
                //注册了一个MiddleA
                context.Use(next1 => {
                    Console.WriteLine("Used Middle B!!!");
                    return async (context1) => {
                        Console.WriteLine("Beging Middle B!!");
                        await next1(context1);
                        Console.WriteLine("Ending Middle B!!");
                    };
                });
                next(context);
                Console.WriteLine("结束过滤器B");
            };
        }
    }

}
