using BeautySalon.Commands;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace BeautySalon.ViewModels
{
    internal class ClientViewModel : ViewModel
    {
        private readonly IClientService clientService;
        private string _title = "Выбор клиента";
        public string Title { get => _title; set => Set(ref _title,value); }


        #region Filter Properties
        private string? lastName;
        private string? firstName;
        private string? patronymic;
        private int age;
        private bool genderM;
        private bool genderW;

        public string? LastName { get => lastName; set { Set(ref lastName, value); clientsViewSource?.View.Refresh(); } }
        public string? FirstName { get => firstName; set { Set(ref firstName, value); clientsViewSource?.View.Refresh(); } }
        public string? Patronymic { get => patronymic; set { Set(ref patronymic, value); clientsViewSource?.View.Refresh(); } }
        public int Age { get => age; set { Set(ref age, value); clientsViewSource?.View.Refresh(); } }
        public bool GenderM { get => genderM; set { Set(ref genderM, value); clientsViewSource?.View.Refresh(); } }
        public bool GenderW { get => genderW; set { Set(ref genderW, value); clientsViewSource?.View.Refresh(); } } 
        #endregion

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

            if (e.Item is Client clientGen)
            {
                if (GenderM)
                    if (clientGen.Gender == Gender.М)
                        e.Accepted = true;
                    else e.Accepted = false;
                if (GenderW)
                    if (clientGen.Gender == Gender.Ж)
                        e.Accepted = true;
                    else e.Accepted = false;
                if (GenderM && GenderW)
                    e.Accepted = true;
            }

            if (e.Item is Client clientAge)
            {
                if (Age > 0)
                {
                    if (clientAge.BirthDay != null)
                    {
                        if (DateTime.Now.Year - clientAge.BirthDay.Value.Year == Age) e.Accepted = true;
                        else e.Accepted = false;
                    }
                    else e.Accepted = false;
                }
            }

        }
        #endregion

        private Client? selectedClient;
        public Client? SelectedClient { get => selectedClient; set=> Set(ref selectedClient,value); }


        #region ClearFilter - Command
        private RelayCommand? clearFilter;
        public RelayCommand? ClearFilterCmd => clearFilter ??= new(obj => ClearFilter(), obj => CanClearFilter());
        private bool CanClearFilter() => LastName != null || FirstName != null || Patronymic != null || Age > 0 || GenderM || GenderW;
        private void ClearFilter()
        {
            LastName = null;
            FirstName = null;
            Patronymic = null;
            Age = 0;
            GenderM = false;
            GenderW = false;
        } 
        #endregion


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
