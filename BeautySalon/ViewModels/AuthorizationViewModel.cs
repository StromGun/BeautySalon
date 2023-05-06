using BeautySalon.Commands;
using BeautySalon.DAL.Context;
using BeautySalon.DAL.Entities;
using BeautySalon.ViewModels.Base;
using System;
using System.Linq;
using System.Windows;

namespace BeautySalon.ViewModels
{
    internal class AuthorizationViewModel : ViewModel
    {
        private readonly BeautySalonDB dB;


        private string? login;
        private string? password;

        public string? Login { get => login; set => Set(ref login,value); }
        public string? Password { get => password; set => Set(ref password, value); }

        public event EventHandler<AuthorizationViewModel>? IsAuthorizated;

        private RelayCommand? loginCmd;
        public RelayCommand? LoginCmd => loginCmd ??= new(obj => LoginExecuted(), obj => !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password));
        private void LoginExecuted()
        {
            try
            {
               var user = dB.Users.FirstOrDefault(x => x.Login == Login && x.Password == Password);
                if (user != null)
                {
                    IsAuthorizated?.Invoke(user, this);
                    Login = null;
                    Password = null;
                }
                else MessageBox.Show("Неверные данные");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public AuthorizationViewModel(BeautySalonDB dB)
        {
            this.dB = dB;

            if (!dB.Users.Any())
            {
                dB.Users.Add(new User { Login = "admin", Password = "admin" });
                dB.SaveChanges();
            }
        }
    }
}
