using BeautySalon.DAL.Context;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using System.Windows;
using System.Linq;

namespace BeautySalon.Commands
{
    class AddOrderCommand : ICommand
    {
        private readonly BeautySalonDB DataBase = App.Services.GetRequiredService<BeautySalonDB>();
        private readonly IUserDialog userDialog = App.Services.GetRequiredService<IUserDialog>();

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            var order = new Order() { };
            if (!userDialog.EditOrder(ref order)) return;

            if (DataBase is null) return;
            order!.OrderName = $"Заказ {order.DateStart!.Value.Date:ddMMyyyy} №{DataBase.Orders.Where(x => x.DateStart!.Value.Date == DateTime.Now.Date).Count() + 1}";

            var results = new List<ValidationResult>();
            var context = new ValidationContext(order!);

            if (!Validator.TryValidateObject(order!, context, results, true))
            {
                foreach (var item in results)
                    MessageBox.Show(item.ErrorMessage);
            }
            else
            {
                try
                {
                    DataBase.Orders.Add(order!);
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
