using System.Windows.Controls;
using System.Windows.Input;
using UI.Infrastructure;
using UI.View.Pages;

namespace UI.ViewModel {
    public class MainViewModel : WindowViewModel {
        private readonly FilmLibraryView _filmLibraryView;
        public MainViewModel(FilmLibraryView filmLibraryView) { _filmLibraryView = filmLibraryView; }
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
        private void ExecuteShowFilmLibrary(object    obj) { CurrentPage = _filmLibraryView; }
        private bool CanExecuteShowFilmLibrary(object obj) => true;
    }
}