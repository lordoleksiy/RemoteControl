using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Owin;
using RemoteControlDesktop.Extensions;
using RemoteControlDesktop.Interfaces;
using RemoteControlDesktop.Services;
using RemoteControlDesktop.ViewModel;
using System;
using System.Windows;
using WindowsInput;
using Dispatcher = System.Windows.Threading.Dispatcher;

namespace RemoteControlDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;
        private IHost _host;
        public App()
        {
            string localIP = StringExtensions.GetLocalIPAddress();
            _host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder => webBuilder
                    .UseUrls($"http://{localIP}:7519")
                    .ConfigureServices(services => {
                        ConfigureServices(services);
                        _serviceProvider = services.BuildServiceProvider();
                        services.AddCors(options =>
                        {
                            options.AddPolicy("AllowAll", builder =>
                            {
                                builder.AllowAnyOrigin()
                                       .AllowAnyMethod()
                                       .AllowAnyHeader();
                            });
                        });
                    })
                    .Configure(app =>
                    {
                        app.UseCors(opt =>
                        {
                            opt.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                        });
                        app.UseRouting();
                        app.UseEndpoints(endpoints => endpoints.MapHub<PositionHub>("/positionHub"));
                    }))
               .Build();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICoordProcessor, CoordProcessor>();
            services.AddSingleton<InputSimulator>();
            services.AddSingleton<Dispatcher>(Dispatcher.CurrentDispatcher);
            services.AddSingleton<PositionHub>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<NotifyIconViewModel>();
            services.AddSignalR();
            services.AddLogging();
        }

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>()
                ?? throw new Exception("main Window not found:(");
            mainWindow.Show();
            await _host.StartAsync();
        }
    }
}
