using Microsoft.Extensions.DependencyInjection;

using Starcounter.Startup.Abstractions;
using Starcounter.Startup.Routing;
using Starcounter.Startup.Routing.Middleware;
using BootstrapExample.ViewModels;

namespace BootstrapExample
{
    public class Startup : IStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddRouter(false)
                .SetMasterPage<MasterNavigationPage>()
                .AddTransient<IPageMiddleware, DbScopeMiddleware>(_ => new DbScopeMiddleware(false))
                .AddTransient<IPageMiddleware, MasterPageMiddleware>()
                .AddContextMiddleware();
        }

        public void Configure(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.ApplicationServices.GetRouter().RegisterAllFromCurrentAssembly();
        }
    }
}