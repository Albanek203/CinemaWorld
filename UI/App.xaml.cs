using System;
using System.Configuration;
using System.Windows;
using DLL.Models;
using Microsoft.Extensions.DependencyInjection;
using UI.Infrastructure;
using UI.View;
using UI.ViewModel;

namespace UI {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public static IServiceProvider ServiceProvider = null!;
        public App() {
            var servicesCollection = new ServiceCollection();
            ConfigServices(servicesCollection);
            ServiceProvider = servicesCollection.BuildServiceProvider();
        }
        private static void ConfigServices(IServiceCollection serviceProvider) {
            BllConfigureService.ConfigureService(serviceProvider
                                               , ConfigurationManager.ConnectionStrings["testConnection"]
                                                                     .ConnectionString);
            serviceProvider.AddSingleton<ThemeViewModel>();
            serviceProvider.AddSingleton<User>();
            serviceProvider.AddTransient<LoginWindow>();
            serviceProvider.AddTransient<MainWindow>();
        }
        private void App_OnStartup(object sender, StartupEventArgs e) {
            // Initialize themes
            var viewModelTheme = ServiceProvider.GetService<ThemeViewModel>();
            ServiceProvider.GetService<LoginWindow>()!.Show();
        }
    }
}