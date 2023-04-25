using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace BeautySalon.ViewModels
{
    internal class ServicesViewModel : ViewModel
    {
        private readonly IServicesService servicesService = App.Services.GetRequiredService<IServicesService>();

        private string title = "Услуги";
        public string Title { get => title; set => Set(ref title,value); }


        private ObservableCollection<Service>? _services;
        public ObservableCollection<Service>? Services { get => _services; set => Set(ref _services, value); }

        private Service? selectedService;
        public Service? SelectedService { get => selectedService; set => Set(ref selectedService, value); }

        private ObservableCollection<ServiceType>? _serviceTypes;
        public ObservableCollection<ServiceType>? ServiceTypes { get => _serviceTypes; set => Set(ref _serviceTypes, value); }

        public ServicesViewModel()
        {
            Services = servicesService.Services;
            ServiceTypes = servicesService.ServiceTypes;
        }

    }
}
