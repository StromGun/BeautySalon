using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;

namespace BeautySalon.ViewModels
{
    internal class ClientListViewModel : ViewModel
    {
        private readonly IClientService clientService = App.Services.GetRequiredService<IClientService>();

        private string title = "Выбор клиента";
        public string Title { get => title; set => title = value; }

        private ObservableCollection<Client>? clients;
        public ObservableCollection<Client>? Clients { get => clients; set => Set(ref clients,value); }

        private Client? selectedClient;
        public Client? SelectedClient { get => selectedClient; set { Set(ref selectedClient, value); UpdateClient(); } }

        private void UpdateClient()
        {
            Client = SelectedClient;
        }

        private Client? client;
        public Client? Client { get => client; set => Set(ref client,value); }

        public ClientListViewModel()
        {
            Clients = clientService.Clients;
        }


    }
}
