using BeautySalon.DAL.Context;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Input;

namespace BeautySalon.Commands
{
    internal class RemoveOrderCommand : ICommand
    {
        private readonly BeautySalonDB DataBase = App.Services.GetRequiredService<BeautySalonDB>();
        private readonly IUserDialog userDialog = App.Services.GetRequiredService<IUserDialog>();

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter) => parameter != null && parameter is Order;

        public void Execute(object? parameter)
        {
            var order = (Order)parameter!;

            if (userDialog.ConfirmedWarning($"Вы точно хотите удалить {order.OrderName}?", "Внимание"))
            {
                try
                {
                    DataBase.Orders.Remove(order!);
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
