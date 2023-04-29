using BeautySalon.DAL.Context;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BeautySalon.Services
{
    class OrdersService : IOrderService, INotifyPropertyChanged
    {
        public BeautySalonDB DB { get; }

        private ObservableCollection<Order>? _orders;
        public ObservableCollection<Order>? Orders { get => _orders; set => Set(ref _orders, value); }

        private ObservableCollection<OrderService>? orderServices;
        public ObservableCollection<OrderService>? OrderServices { get => orderServices; set => Set(ref orderServices,value); }

        public ObservableCollection<Order>? GetOrders()
        {
            DB.Orders.Load();
            return DB.Orders.Local.ToObservableCollection();
        }

        public ObservableCollection<OrderService>? GetOrderServices()
        {
            DB.OrderServices.Include(e => e.Service).Load();
            return DB.OrderServices.Local.ToObservableCollection();
        }

        public ObservableCollection<OrderService>? GetSpecificOrderServices(Order order)
        {
            if (order == null) return null;
            DB.OrderServices.Where(e => e.Order == order).Include(e => e.Service).Load();
            return DB.OrderServices.Local.ToObservableCollection();
        }

        public OrdersService(BeautySalonDB dB)
        {
            DB = dB;
            Orders = GetOrders();
            OrderServices = GetOrderServices();
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
