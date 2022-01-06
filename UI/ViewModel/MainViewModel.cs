using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using UI.Infrastructure;
using UI.View;
using UI.View.Pages;

namespace UI.ViewModel {
    public class MainViewModel : WindowViewModel {
        private readonly SessionLibraryView _sessionLibraryView;
        private readonly LoginView _loginView;
        public MainViewModel(SessionLibraryView sessionLibraryView, LoginView loginView) {
            _sessionLibraryView = sessionLibraryView;
            _loginView          = loginView;
        }
        private Page _currentPage;
        public Page CurrentPage {
            get => _currentPage;
            set {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        private RelayCommand _showFilmLibrary;
        public ICommand ShowFilmLibrary =>
            _showFilmLibrary ??= new RelayCommand(ExecuteShowFilmLibrary, CanExecuteShowFilmLibrary);
        private void ExecuteShowFilmLibrary(object    obj) { CurrentPage = _sessionLibraryView; }
        private bool CanExecuteShowFilmLibrary(object obj) => true;
        private RelayCommand _logOut;
        public ICommand LogOut => _logOut ??= new RelayCommand(ExecuteLogOut, CanExecuteLogOut);
        private void ExecuteLogOut(object obj) {
            if (File.Exists(App.SaveUserDataPath)) File.Delete(App.SaveUserDataPath);
            _loginView.Show();
            App.MainWindow.Close();
        }
        private bool CanExecuteLogOut(object obj) => true;
    }
}