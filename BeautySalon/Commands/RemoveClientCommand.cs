using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Input;
using BeautySalon.DAL.Context;
using BeautySalon.DAL.Entities;

namespace BeautySalon.Commands
{
    internal class RemoveClientCommand : ICommand
    {
        private readonly BeautySalonDB DataBase = App.Services.GetRequiredService<BeautySalonDB>();

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter) => parameter != null && parameter is Client;

        public void Execute(object? parameter)
        {
            var client = (Client)parameter!;

            if (MessageBox.Show($"Вы действительн хотите удалить клиента {client?.FirstName}?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                try
                {
                    DataBase.Remove(client!);
                    DataBase.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
