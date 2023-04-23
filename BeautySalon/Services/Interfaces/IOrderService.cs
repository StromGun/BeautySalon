using BeautySalon.DAL.Entities;
using System.Collections.ObjectModel;

namespace BeautySalon.Services.Interfaces
{
    interface IOrderService
    {
        public ObservableCollection<Order>? Orders { get; set; }
        protected ObservableCollection<Order>? GetOrders();
    }
}
