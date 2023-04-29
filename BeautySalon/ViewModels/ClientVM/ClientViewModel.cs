using BeautySalon.Commands;
using BeautySalon.DAL.Context;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace BeautySalon.ViewModels
{
    internal class ClientViewModel : ViewModel
    {
        private readonly IClientService clientService;
        private string _title = "Dada";
        public string Title { get => _title; set => Set(ref _title,value); }

        #region Clients
        private CollectionViewSource? clientsViewSource;
        private ObservableCollection<Client>? clients;
        public ICollectionView? ClientsView => clientsViewSource?.View;
        public ObservableCollection<Client>? Clients
        {
            get => clients;
            set
            {
                if (Set(ref clients, value))
                {
                    clientsViewSource = new CollectionViewSource
                    {
                        Source = value,
                        SortDescriptions = { new SortDescription(nameof(Client.ID), ListSortDirection.Ascending) }
                    };

                    clientsViewSource?.View.Refresh();
                    OnPropertyChanged(nameof(ClientsView));
                }
            }
        } 
        #endregion

        private Client? selectedClient;
        public Client? SelectedClient { get => selectedClient; set=> Set(ref selectedClient,value); }

        #region Loaded
        private RelayCommand? loadedCmd;
        public RelayCommand? LoadedCmd => loadedCmd ??= new RelayCommand(obj => Loaded());
        private void Loaded()
        {
            Clients = clientService.Clients;
        }
        #endregion

       
        public ClientViewModel(IClientService clientService)
        {
            this.clientService = clientService;
        }


    }
}
