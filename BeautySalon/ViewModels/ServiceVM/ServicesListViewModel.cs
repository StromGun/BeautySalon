using BeautySalon.Commands;
using BeautySalon.DAL.Context;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace BeautySalon.ViewModels
{
    internal class ServicesListViewModel : ViewModel
    {
        private readonly IServicesService servicesService;
        private readonly IUserDialog userDialog;
        private readonly BeautySalonDB dB;


        #region Services
        private ObservableCollection<Service>? services;
        public ObservableCollection<Service>? Services
        {
            get => services;
            set
            {
                if (Set(ref services, value))
                    servicesViewSource = new CollectionViewSource
                    {
                        Source = value,
                        SortDescriptions = { new SortDescription(nameof(Service.ID), ListSortDirection.Ascending) }
                    };

                servicesViewSource.Filter += ServicesViewSource_Filter;

                servicesViewSource?.View.Refresh();
                OnPropertyChanged(nameof(ServicesView));
            }
        }

        private void ServicesViewSource_Filter(object sender, FilterEventArgs e)
        {
            if (e.Item is not Service service) return;
            if (service.ServiceType != SelectedServiceType)
                e.Accepted = false;
        }
        private CollectionViewSource? servicesViewSource;
        public ICollectionView? ServicesView => servicesViewSource?.View; 
        #endregion

        private ObservableCollection<ServiceType>? serviceTypes;
        public ObservableCollection<ServiceType>? ServiceTypes { get => serviceTypes; set => Set(ref serviceTypes, value); }

        private ServiceType? selectedServiceType;
        public ServiceType? SelectedServiceType { get => selectedServiceType; set { Set(ref selectedServiceType, value); servicesViewSource?.View.Refresh(); } }
        private Service? selectedService;
        public Service? SelectedService { get => selectedService; set => Set(ref selectedService, value); }


        #region AddServiceType - Command
        private RelayCommand? addServiceType;
        public RelayCommand? AddServiceTypeCmd => addServiceType ??= new(obj => AddNewServiceType());
        private void AddNewServiceType()
        {
            ServiceType newServiceType = new();
            if (!userDialog.EditServiceCategory(ref newServiceType)) return;

            try
            {
                dB.ServiceTypes.Add(newServiceType);
                dB.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region RemoveServiceType - Command
        private RelayCommand? removeServiceType;
        public RelayCommand? RemoveServiceTypeCmd => removeServiceType ??= new(obj => RemoveServiceType(),
            obj => SelectedServiceType is ServiceType && SelectedServiceType != null);
        private void RemoveServiceType()
        {
            if (!userDialog.ConfirmedInformation($"Вы точно хотите удалить категорию услуг: {SelectedServiceType!.Name}?", "Внимание")) return;
            try
            {
                dB.ServiceTypes.Remove(SelectedServiceType!);
                dB.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
        #endregion

        #region AddService - Command
        private RelayCommand? addService;
        public RelayCommand? AddServiceCmd => addService ??= new(obj => AddNewService());
        private void AddNewService()
        {
            Service newService = new();
            if (!userDialog.EditService(ref newService)) return;

            try
            {
                dB.Services.Add(newService);
                dB.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region RemoveService - Command
        private RelayCommand? removeService;
        public RelayCommand? RemoveServiceCmd => removeService ??= new(obj => RemoveService(),
            obj => SelectedService is Service && SelectedService != null);
        private void RemoveService()
        {
            if (!userDialog.ConfirmedInformation($"Вы точно хотите удалить услугу: {SelectedService!.Name}?", "Внимание")) return;
            try
            {
                dB.Services.Remove(SelectedService!);
                dB.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion



        public ServicesListViewModel(IServicesService servicesService, IUserDialog userDialog, BeautySalonDB dB)
        {
            this.servicesService = servicesService;
            this.userDialog = userDialog;
            this.dB = dB;

            Services = this.servicesService.Services;
            ServiceTypes = this.servicesService.ServiceTypes;
        }
    }
}
