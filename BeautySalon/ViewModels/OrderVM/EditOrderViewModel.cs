using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace BeautySalon.ViewModels
{
    internal class EditOrderViewModel : ViewModel
    {
        private readonly IOrderService orderService = App.Services.GetRequiredService<IOrderService>();


        private ObservableCollection<OrderService>? orderServiceCollection;
        public ObservableCollection<OrderService>? OrderService { get => orderServiceCollection; set => Set(ref orderServiceCollection,value); }

        private Order? order;
        public Order? Order { get => order; set => Set(ref order,value); }

        public EditOrderViewModel(Order? order)
        {
            Order = order;

            OrderService = orderService.GetSpecificOrderServices(Order!);
        }
    }
}
