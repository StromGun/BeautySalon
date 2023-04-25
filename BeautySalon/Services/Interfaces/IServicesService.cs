using BeautySalon.DAL.Entities;
using System.Collections.ObjectModel;

namespace BeautySalon.Services.Interfaces
{
    interface IServicesService
    {
        public ObservableCollection<Service>? Services { get; protected set; }
        public ObservableCollection<ServiceType>? ServiceTypes { get; protected set; }

        protected ObservableCollection<Service>? GetServices();
        protected ObservableCollection<ServiceType>? GetServiceTypes();
    }
}
