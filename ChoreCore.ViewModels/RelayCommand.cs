using System;
using System.Windows.Input;

namespace ChoreCore.ViewModels
{
    public class RelayCommand : ICommand
    {
        private Action _methodToExecute;
        private Func<bool> _canExecuteEvaluator;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
        {
            _methodToExecute = methodToExecute;
            _canExecuteEvaluator = canExecuteEvaluator;
        }

        public RelayCommand(Action methodToExecute) : this(methodToExecute, null) { }

        public bool CanExecute(object parameter)
        {
            return _canExecuteEvaluator == null ? true : _canExecuteEvaluator.Invoke();
        }

        public void Execute(object parameter)
        {
            _methodToExecute.Invoke();
        }
    }
}
