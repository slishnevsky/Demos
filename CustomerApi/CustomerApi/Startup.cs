using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using CustomerApi.DependencyResolution;
using CustomerApi.Service;
using Microsoft.Owin;
using Owin;
using StructureMap;

[assembly: OwinStartup(typeof(CustomerApi.Startup))]

namespace CustomerApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<CustomerService>().AsSelf().InstancePerRequest();
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        public void Configuration2(IAppBuilder appBuilder)
        {
            StructureMap.IContainer container = IoC.Initialize();

            container.AssertConfigurationIsValid();

            var types = container.WhatDoIHave();

            Debug.WriteLine(container.WhatDoIHave());

            HttpConfiguration httpConfiguration = new HttpConfiguration
            {
                DependencyResolver = new StructureMapWebApiDependencyResolver(container)
            };

            WebApiConfig.Register(httpConfiguration);

            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}
