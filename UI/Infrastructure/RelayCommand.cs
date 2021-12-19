using System;
using System.Windows.Input;

namespace UI.Infrastructure;

public class RelayCommand {
    private readonly Action<object>    _execute;
    private readonly Predicate<object> _canExecute;
    public RelayCommand(Action<object> execute, Predicate<object> canExecute = null) {
        _execute = execute ?? throw new ArgumentNullException();
        _canExecute = canExecute;
    }
    public bool CanExecute(object parameter) { return _canExecute == null || _canExecute.Invoke(parameter); }
    public void Execute(object parameter)    { _execute.Invoke(parameter); }
    public event EventHandler CanExecuteChanged {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}