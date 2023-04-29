using BeautySalon.DAL.Context;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Input;

namespace BeautySalon.Commands
{
    public class EditOrderCommand : ICommand
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
            var order = parameter as Order;
            if (!userDialog.EditOrder(ref order)) return;

            //var results = new List<ValidationResult>();
            //var context = new ValidationContext(order!);

            //if (!Validator.TryValidateObject(order!, context, results, true))
            //{
            //    foreach (var item in results)
            //        MessageBox.Show(item.ErrorMessage);
            //}
            //else
            //{
            //    try
            //    {
            //        DataBase.Orders.Update(order!);
            //        DataBase.SaveChanges();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }
    }
}
