using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Starcounter;
using Starcounter.Startup.Abstractions;
using Starcounter.Startup;

namespace BootstrapExample
{
    public class CustomAppShellBootstrapper
    {
        public static void Start(IStartup application)
        {
            Application.Current.Use(new HtmlFromJsonProvider());
            Application.Current.Use(new PartialToStandaloneHtmlProvider(
                File.ReadAllText(Path.Combine(Application.Current.WorkingDirectory, @"wwwroot\BootstrapExample\views\AppShell.html"))));

            var services = new ServiceCollection();
            AddDefaultServices(services);
            application.ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<ILogger<DefaultStarcounterBootstrapper>>();
            logger.LogInformation($"Configuring application {application}");
            var applicationBuilder = new ApplicationBuilder()
            {
                ApplicationServices = serviceProvider
            };
            Action<IApplicationBuilder> configure = application.Configure;
            foreach (var startupFilter in serviceProvider.GetServices<IStartupFilter>().Reverse())
            {
                configure = startupFilter.Configure(configure);
            }

            configure(applicationBuilder);
            logger.LogInformation($"Started application {application}");
        }

        private static void AddDefaultServices(IServiceCollection services)
        {
            services
                .AddOptions()
                .AddLogging(logging => logging.AddConsole());
        }
    }
}