using System;
using System.Windows.Input;

namespace BeautySalon.Commands
{
    internal class DialogResultCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool? DialogResult { get; set; }

        public bool CanExecute(object? parameter) => App.CurrentWindow != null;

        public void Execute(object? parameter)
        {
            if (!CanExecute(parameter)) return;

            var window = App.CurrentWindow;

            var dialog_result = DialogResult;
            if (parameter != null)
                dialog_result = Convert.ToBoolean(parameter);

            window!.DialogResult = dialog_result;
            window.Close();
        }
    }
}
