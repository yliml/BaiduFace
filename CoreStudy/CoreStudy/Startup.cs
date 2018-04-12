using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoreStudyComm;

namespace CoreStudy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }


        public void ConfigureStagingServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IPeople>(new People("Staging"));
            services.AddSingleton<IPeople>(new People("StagingAAA"));
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IPeople>(new People("Development"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IPeople people)
        {
            people.SayHello();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.Use(next =>
            {
                Console.WriteLine("Used Middle A!!!");
                return async (context) =>
                {
                    Console.WriteLine("Beging Middle A!!");
                    await next(context);
                    Console.WriteLine("Ending Middle A!!");
                };
            });
            app.UseWhen(http => http.Request.Path.StartsWithSegments("/User"), appBuilder => Console.WriteLine("Used UseWhen Middleware"));

            app.UseWhen(http => http.Request.Path.StartsWithSegments("/User"), appBuilder => Console.WriteLine("Used UseWhen Middleware"));

            app.Use(next =>
            {
                Console.WriteLine("Used Middle B!!!");
                return async (context) =>
                {
                    Console.WriteLine("Beging Middle B!!");
                    await next(context);
                    Console.WriteLine("Ending Middle B!!");
                };
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

        }
    }
}
