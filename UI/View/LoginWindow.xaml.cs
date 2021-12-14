using System.IO;
using System.Windows;
using System.Windows.Input;
using BLL.Services;
using DLL.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace UI.View {
    public partial class LoginWindow : Window {
        private readonly LoginService _loginService;
        public LoginWindow(LoginService loginService) {
            InitializeComponent();
            _loginService = loginService;
            MessageBox.Show(Directory.GetCurrentDirectory());
        }

#region Window Control
        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)    { Application.Current.Shutdown(); }
        private void ButtonCollapse_OnClick(object sender, RoutedEventArgs e) { WindowState = WindowState.Minimized; }
        private void UILogin_OnMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
#endregion

        private async void ButtonLogin_OnClick(object sender, RoutedEventArgs e) {
            TxtException.Text = string.Empty;
            if (string.IsNullOrWhiteSpace(TxtUserLogin.Text) || string.IsNullOrWhiteSpace(TxtUserPassword.Password)) {
                TxtException.Text = "You have not entered a value";
                return;
            }
            var user = await _loginService.Login(TxtUserLogin.Text, TxtUserPassword.Password);
            if (user == null) {
                TxtException.Text = "Invalid Login or Password. Please try again.";
                return;
            }
            var curUser = App.ServiceProvider.GetService<User>();
            curUser!.Id = user.Id;
            curUser.Person = user.Person;
            curUser.Role = user.Role;
            curUser.ActionsData = user.ActionsData;
            curUser.LoginData = user.LoginData;
            App.ServiceProvider.GetService<MainWindow>()!.Show();
            if (IsSaveUser.IsChecked == true) SaveLoginUser(user);
            Close();
        }
        private async void SaveLoginUser(User user) {
            var jsonString = JsonConvert.SerializeObject(user);
            await File.WriteAllTextAsync(Directory.GetCurrentDirectory() + "\\saveLU.data", jsonString);
        }
    }
}