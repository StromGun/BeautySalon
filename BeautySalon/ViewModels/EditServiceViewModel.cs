using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;

namespace BeautySalon.ViewModels
{
    internal class EditServiceViewModel : ViewModel
    {
        private readonly IServicesService _servicesService = App.Services.GetRequiredService<IServicesService>();

        private Service? service;
        private ObservableCollection<ServiceType>? serviceTypes;

        public Service? Service { get => service; set => Set(ref service,value); }
        public ObservableCollection<ServiceType>? ServiceTypes { get => serviceTypes; set => Set(ref serviceTypes, value); }

        public EditServiceViewModel(Service service)
        {
            ServiceTypes = _servicesService.ServiceTypes;

            Service = new Service()
            {
                Name = service.Name,
                Price = service.Price,
                ServiceType = service.ServiceType,
                Time = service.Time == null ? new TimeSpan(0,0,0) : service.Time,
            };
        }
    }
}
