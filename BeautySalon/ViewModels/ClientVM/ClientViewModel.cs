using BeautySalon.Commands;
using BeautySalon.DAL.Context;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using System.Collections.Generic;
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


        private string? lastName;
        private string? firstName;
        private string? patronymic;
        private int age;
        private Gender gender;

        public string? LastName { get => lastName; set { Set(ref lastName, value); clientsViewSource?.View.Refresh(); } }
        public string? FirstName { get => firstName; set { Set(ref firstName, value); clientsViewSource?.View.Refresh(); } }
        public string? Patronymic { get => patronymic; set { Set(ref patronymic, value); clientsViewSource?.View.Refresh(); } }
        public int Age { get => age; set { Set(ref age, value); clientsViewSource?.View.Refresh(); } }
        public Gender Gender { get => gender; set { Set(ref gender, value); clientsViewSource?.View.Refresh(); } }

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

                    clientsViewSource.Filter += ClientsViewSource_Filter;

                    clientsViewSource?.View.Refresh();
                    OnPropertyChanged(nameof(ClientsView));
                }
            }
        }

        private void ClientsViewSource_Filter(object sender, FilterEventArgs e)
        {
            if (e.Item is Client clientLM && !string.IsNullOrEmpty(LastName))
            {
                if (!clientLM.LastName!.ToLower().Contains(LastName.ToLower()))
                    e.Accepted = false;
            }

            if (e.Item is Client clientFM && !string.IsNullOrEmpty(FirstName))
            {
                if (!clientFM.FirstName!.ToLower().Contains(FirstName.ToLower()))
                    e.Accepted = false;
            }

            if (e.Item is Client clientPT && !string.IsNullOrEmpty(Patronymic))
            {
                if (clientPT.Patronymic == null) e.Accepted = false;
                else if (!clientPT.Patronymic.ToLower().Contains(Patronymic.ToLower()))
                    e.Accepted = false;
            }

            //if (e.Item is Client clientGen)
            //{
            //    if (clientGen.Gender != Gender)
            //        e.Accepted = false;
            //}
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
