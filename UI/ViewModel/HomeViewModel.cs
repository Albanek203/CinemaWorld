using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Infrastructure;

namespace UI.ViewModel {
    public class HomeViewModel : BaseViewModel {
        public HomeViewModel() {
            Task.Factory.StartNew(() => {
                while (true) {
                    Thread.Sleep(1000);
                    var date = DateTime.Now;
                    Hour   = date.Hour;
                    Minute = date.Minute;
                    Second = date.Second;
                }
            });
        }
        private RelayCommand _throwInstagram;
        public ICommand ThrowInstagram =>
            _throwInstagram ??= new RelayCommand(ExecuteThrowInstagram, CanExecuteThrowInstagram);
        private void ExecuteThrowInstagram(object    obj) { OpenUrl("https://www.instagram.com"); }
        private bool CanExecuteThrowInstagram(object obj) => true;
        private RelayCommand _throwTwitter;
        public ICommand ThrowTwitter => _throwTwitter ??= new RelayCommand(ExecuteThrowTwitter, CanExecuteThrowTwitter);
        private void ExecuteThrowTwitter(object    obj) { OpenUrl("https://twitter.com"); }
        private bool CanExecuteThrowTwitter(object obj) => true;
        private RelayCommand _throwFacebook;
        public ICommand ThrowFacebook =>
            _throwFacebook ??= new RelayCommand(ExecuteThrowFacebook, CanExecuteThrowFacebook);
        private void ExecuteThrowFacebook(object    obj) { OpenUrl("https://facebook.com"); }
        private bool CanExecuteThrowFacebook(object obj) => true;
        private RelayCommand _throwYoutube;
        public ICommand ThrowYoutube => _throwYoutube ??= new RelayCommand(ExecuteThrowYoutube, CanExecuteThrowYoutube);
        private void ExecuteThrowYoutube(object    obj) { OpenUrl("https://youtube.com"); }
        private bool CanExecuteThrowYoutube(object obj) => true;
        private void OpenUrl(string url) {
            try { Process.Start(url); } catch {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) { Process.Start("xdg-open", url); }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) { Process.Start("open", url); }
                else { throw; }
            }
        }
        private int _second;
        public int Second {
            get => _second;
            set {
                _second = value;
                OnPropertyChanged(nameof(Second));
            }
        }
        private int _minute;
        public int Minute {
            get => _minute;
            set {
                _minute = value;
                OnPropertyChanged(nameof(Minute));
            }
        }
        private int _hour;
        public int Hour {
            get => _hour;
            set {
                _hour = value;
                OnPropertyChanged(nameof(Hour));
            }
        }
    }
}