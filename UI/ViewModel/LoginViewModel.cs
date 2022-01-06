using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BLL.Services;
using DLL.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using UI.Infrastructure;
using UI.View;

namespace UI.ViewModel {
    public class LoginViewModel : WindowViewModel {
        private readonly string _saveUserDataPath = Directory.GetCurrentDirectory() + "\\saveLU.data";
        private Window? _currentWindow;
        private readonly LoginService _loginService;
        public LoginViewModel(LoginService loginService) { _loginService = loginService; }
        private string _userLogin;
        public string UserLogin {
            get => _userLogin;
            set {
                _userLogin = value;
                OnPropertyChanged(nameof(UserLogin));
            }
        }
        private string _exception;
        public string Exception {
            get => _exception;
            set {
                _exception = value;
                OnPropertyChanged(nameof(Exception));
            }
        }
        private bool _isSaveUser;
        public bool IsSaveUser {
            get => _isSaveUser;
            set {
                _isSaveUser = value;
                OnPropertyChanged(nameof(IsSaveUser));
            }
        }
        private RelayCommand _authorization;
        public ICommand Authorization =>
            _authorization ??= new RelayCommand(ExecuteAuthorization, CanExecuteAuthorization);
        private async void ExecuteAuthorization(object obj) {
            var password = (obj as PasswordBox)!.Password;
            Exception = string.Empty;
            var user = await _loginService.LoginAsync(UserLogin, password);
            if (user == null) {
                Exception = "Invalid Login or Password. Please try again.";
                return;
            }
            if (IsSaveUser) {
                try {
                    var jsonString = JsonConvert.SerializeObject(user);
                    await File.WriteAllTextAsync(_saveUserDataPath, jsonString);
                } catch (Exception e) {
                    Debug.Write(e.Message);
                    return;
                }
            }
            var curUser = App.ServiceProvider.GetService<User>();
            curUser!.Id         = user.Id;
            curUser.Person      = user.Person;
            curUser.Role        = user.Role;
            curUser.ActionsData = user.ActionsData;
            curUser.LoginData   = user.LoginData;
            App.ServiceProvider.GetService<MainView>()!.Show();
            _currentWindow!.Close();
        }
        private bool CanExecuteAuthorization(object obj) {
            var password = (obj as PasswordBox)!.Password;
            return !string.IsNullOrWhiteSpace(UserLogin) && !string.IsNullOrWhiteSpace(password);
        }
    }
}