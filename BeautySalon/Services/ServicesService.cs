using BeautySalon.DAL.Context;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeautySalon.Services
{
    class ServicesService : IServicesService, INotifyPropertyChanged
    {
        private ObservableCollection<Service>? _services;
        public ObservableCollection<Service>? Services { get => _services; set => Set(ref _services, value); }

        private ObservableCollection<ServiceType>? _serviceTypes;
        public ObservableCollection<ServiceType>? ServiceTypes { get => _serviceTypes; set => Set(ref _serviceTypes, value); }

        public BeautySalonDB DB { get; }

        public ObservableCollection<Service>? GetServices()
        {
            DB.Services.Load();
            return DB.Services.Local.ToObservableCollection();
        }

        public ObservableCollection<ServiceType>? GetServiceTypes()
        {
            DB.ServiceTypes.Load();
            return DB.ServiceTypes.Local.ToObservableCollection();
        }

        public ServicesService(BeautySalonDB DB)
        {
            this.DB = DB;
            Services = GetServices();
            ServiceTypes = GetServiceTypes();
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
