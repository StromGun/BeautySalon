using BeautySalon.DAL.Context;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BeautySalon.Commands
{
    internal class EditClientCommand : ICommand
    {
        private readonly BeautySalonDB DataBase = App.Services.GetRequiredService<BeautySalonDB>();
        private readonly IUserDialog userDialog = App.Services.GetRequiredService<IUserDialog>();

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter) => parameter != null && parameter is Client;

        public void Execute(object? parameter)
        {
            if (!userDialog.EditClient((Client)parameter!)) return;

            var results = new List<ValidationResult>();
            var context = new ValidationContext(parameter!);

            if (!Validator.TryValidateObject(parameter!, context, results, true))
            {
                foreach (var item in results)
                    MessageBox.Show(item.ErrorMessage);
            }
            else
            {
                try
                {
                    DataBase.Clients.Update((Client)parameter!);
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
