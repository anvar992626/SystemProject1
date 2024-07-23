using DataLager;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;


namespace WpfApp
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Register your services, for example:
                    services.AddDbContext<SkiContext>();
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Start the host
            _host.Start();

            // Create a service scope to get the SkiContext
            using (var serviceScope = _host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var context = services.GetRequiredService<SkiContext>();
                // Ensure the database is created
                context.Database.EnsureCreated();
                // Seed the data
                SkiContext.SeedData(context);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Stop the host when the application is closing
            _host.Dispose();
            base.OnExit(e);
        }

        // ... rest of the class ...
    }
}
