using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace BeautySalon.ViewModels
{
    internal class ServicesListViewModel : ViewModel
    {
        private readonly IServicesService servicesService;


        private ObservableCollection<Service>? services;
        public ObservableCollection<Service>? Services { get => services;
            set {
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

        private ObservableCollection<ServiceType>? serviceTypes;
        public ObservableCollection<ServiceType>? ServiceTypes { get => serviceTypes; set => Set(ref serviceTypes, value); }

        private ServiceType? selectedServiceType;
        public ServiceType? SelectedServiceType { get => selectedServiceType; set { Set(ref selectedServiceType, value); servicesViewSource?.View.Refresh(); } }

        public ServicesListViewModel(IServicesService servicesService)
        {
            this.servicesService = servicesService;

            Services = this.servicesService.Services;
            ServiceTypes = this.servicesService.ServiceTypes;
        }
    }
}
