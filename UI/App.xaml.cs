using System;
using System.Configuration;
using System.IO;
using System.Windows;
using DLL.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using UI.Infrastructure;
using UI.View;
using UI.View.Pages;
using UI.ViewModel;

namespace UI {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private readonly string _saveUserDataPath = Directory.GetCurrentDirectory() + "\\saveLU.data";
        public static IServiceProvider ServiceProvider = null!;
        public static Window MainWindow;
        public App() {
            var servicesCollection = new ServiceCollection();
            ConfigServices(servicesCollection);
            ServiceProvider = servicesCollection.BuildServiceProvider();
        }
        private static void ConfigServices(IServiceCollection serviceProvider) {
            BllConfigureService.ConfigureService(serviceProvider
                                               , ConfigurationManager.ConnectionStrings["testConnection"]
                                                     .ConnectionString);
            // Models
            serviceProvider.AddSingleton<User>();

            // View models
            serviceProvider.AddSingleton<ThemeViewModel>();
            serviceProvider.AddTransient<SessionLibraryViewModel>();
            serviceProvider.AddTransient<LoginViewModel>();
            serviceProvider.AddTransient<MainViewModel>();
            serviceProvider.AddTransient<SellTicketViewModel>();

            // Pages
            serviceProvider.AddTransient<SessionLibraryView>();

            // Views
            serviceProvider.AddTransient<LoginView>();
            serviceProvider.AddTransient<MainView>();
        }
        private void App_OnStartup(object sender, StartupEventArgs e) {
            // Initialize themes
            var viewModelTheme = ServiceProvider.GetService<ThemeViewModel>();
            if (File.Exists(_saveUserDataPath)) {
                var user    = JsonConvert.DeserializeObject<User>(File.ReadAllText(_saveUserDataPath));
                var curUser = ServiceProvider.GetService<User>();
                curUser!.Id         = user!.Id;
                curUser.Person      = user.Person;
                curUser.Role        = user.Role;
                curUser.ActionsData = user.ActionsData;
                curUser.LoginData   = user.LoginData;
                MainWindow          = ServiceProvider.GetService<MainView>()!;
                MainWindow.Show();
            }
            else
                ServiceProvider.GetService<LoginView>()!.Show();
        }
    }
}