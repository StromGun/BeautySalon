using BeautySalon.DAL.Context;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeautySalon.Services
{
    internal class ClientService : IClientService, INotifyPropertyChanged
    {
        private ObservableCollection<Client>? _clients;
        private readonly BeautySalonDB dB;

        public ObservableCollection<Client>? Clients { get => _clients; set => Set(ref _clients,value); }

        public ObservableCollection<Client>? GetClients()
        {
            dB.Clients.Load();
            return  dB.Clients.Local.ToObservableCollection();
        }

        public ClientService(BeautySalonDB dB)
        {
            this.dB = dB;
            Clients = GetClients();
        }


        #region Dda

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public bool Set<T>(ref T field, T value, [CallerMemberName] string? prop = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(prop);
            return true;
        }

        
        #endregion
    }
}
