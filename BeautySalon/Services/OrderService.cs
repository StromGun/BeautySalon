using BeautySalon.DAL.Context;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeautySalon.Services
{
    class OrderService : IOrderService, INotifyPropertyChanged
    {
        private ObservableCollection<Order>? _orders;
        public ObservableCollection<Order>? Orders { get => _orders; set => Set(ref _orders, value); }
        public BeautySalonDB DB { get; }

        public ObservableCollection<Order>? GetOrders()
        {
            DB.Orders.Load();
            return DB.Orders.Local.ToObservableCollection();
        }

        public OrderService(BeautySalonDB dB)
        {
            DB = dB;
            GetOrders();
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
