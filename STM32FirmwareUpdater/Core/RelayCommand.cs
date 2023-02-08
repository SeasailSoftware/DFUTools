using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace STM32FirmwareUpdater.Core
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canexecute;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public RelayCommand(Action<object> action)
        {
            _execute = action;
            _canexecute = null;
        }
        public RelayCommand(Action<object> action, Predicate<object> canAction)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            _execute = action;
            _canexecute = canAction;
        }
        public bool CanExecute(object para)
        {
            return _canexecute == null ? true : _canexecute(para);
        }
        public void Execute(object para)
        {
            _execute(para);
        }
    }
}
