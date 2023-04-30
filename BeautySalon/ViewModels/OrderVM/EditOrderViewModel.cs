using BeautySalon.DAL.Entities;
using BeautySalon.ViewModels.Base;
using System.Collections.ObjectModel;

namespace BeautySalon.ViewModels
{
    internal class EditOrderViewModel : ViewModel
    {

        private ObservableCollection<OrderService>? orderServiceCollection;
        public ObservableCollection<OrderService>? OrderService { get => orderServiceCollection; set => Set(ref orderServiceCollection,value); }

        private Order? order;
        public Order? Order { get => order; set => Set(ref order,value); }

        public EditOrderViewModel(Order? order)
        {
            Order = new()
            {
                ID = order.ID,
                Client = order.Client,
                DateStart = order.DateStart,
                OrderName = order.OrderName,
                OrderServices = order.OrderServices,
                Services = order.Services,
                Status = order.Status,
                TimeEnd = order.TimeEnd,
                TotalPrice = order.TotalPrice
            };

            if (order.OrderServices != null)
                OrderService = new(order.OrderServices);
            else OrderService = new();
        }
    }
}
