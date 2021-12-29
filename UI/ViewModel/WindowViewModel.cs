using System.Windows;
using System.Windows.Input;
using UI.Infrastructure;

namespace UI.ViewModel {
    public abstract class WindowViewModel : BaseViewModel {
        private Window? _currentWindow;
        private RelayCommand _closeWindow;
        public ICommand CloseWindow => _closeWindow ??= new RelayCommand(ExecuteCloseWindow, CanExecuteCloseWindow);
        private void ExecuteCloseWindow(object    obj) { Application.Current.Shutdown(); }
        private bool CanExecuteCloseWindow(object obj) => true;
        private RelayCommand _collapseWindow;
        public ICommand CollapseWindow =>
            _collapseWindow ??= new RelayCommand(ExecuteCollapseWindow, CanExecuteCollapseWindow);
        private void ExecuteCollapseWindow(object obj) { (obj as Window)!.WindowState = WindowState.Minimized; }
        private bool CanExecuteCollapseWindow(object obj) {
            _currentWindow ??= obj as Window;
            return true;
        }
    }
}