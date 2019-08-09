using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using OwinDemo.Middleware;

[assembly: OwinStartup(typeof(OwinDemo.Startup))]

namespace OwinDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.Use<DebugMiddleware>(new DebugMiddlewareOptions
            //{
            //    OnIncomingRequest = (ctx) =>
            //    {
            //        var watch = new Stopwatch();
            //        watch.Start();
            //        ctx.Environment["DebugStopwatch"] = watch;
            //    },
            //    OnOutgoingRequest = (ctx) =>
            //    {
            //        var watch = (Stopwatch)ctx.Environment["DebugStopwatch"];
            //        watch.Stop();
            //        Debug.WriteLine("Request took: " + watch.ElapsedMilliseconds + " ms");
            //    },
            //    Welcome = (ctx) =>
            //    {
            //        Debug.WriteLine("Welcome");
            //    }
            //});

            //app.UseDebugMiddleware instead of app.Use<DebugMiddleware>
            app.UseDebugMiddleware(new DebugMiddlewareOptions
            {
                OnIncomingRequest = (ctx) =>
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    ctx.Environment["DebugStopwatch"] = watch;
                },
                OnOutgoingRequest = (ctx) =>
                {
                    var watch = (Stopwatch)ctx.Environment["DebugStopwatch"];
                    watch.Stop();
                    Debug.WriteLine("Request took: " + watch.ElapsedMilliseconds + " ms");
                }
            });

            app.UseNancy();

            app.Use(async (ctx, next) =>
            {
                await ctx.Response.WriteAsync("Hello World!");
            });
        }
    }
}
