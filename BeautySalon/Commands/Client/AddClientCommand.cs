using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows;
using BeautySalon.DAL.Context;

namespace BeautySalon.Commands
{
    internal class AddClientCommand : ICommand
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
            var client = new Client();
            if (!userDialog.EditClient(client)) return;

            if (DataBase is null) return;

            var results = new List<ValidationResult>();
            var context = new ValidationContext(client);

            if (!Validator.TryValidateObject(client, context, results, true))
            {
                foreach (var item in results)
                    MessageBox.Show(item.ErrorMessage);
            }
            else
            {
                try
                {
                    DataBase.Clients.Add(client);
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
