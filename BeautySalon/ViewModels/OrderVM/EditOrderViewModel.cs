using BeautySalon.Commands;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace BeautySalon.ViewModels
{
    internal class EditOrderViewModel : ViewModel
    {
        private readonly IUserDialog userDialog = App.Services.GetRequiredService<IUserDialog>();


        private ObservableCollection<OrderService>? orderServiceCollection;
        public ObservableCollection<OrderService>? OrderService { get => orderServiceCollection; set => Set(ref orderServiceCollection,value); }

        private Order? order;
        public Order? Order { get => order; set => Set(ref order,value); }

        private OrderService? selectedOrderService;
        public OrderService? SelectedOrderService { get => selectedOrderService; set => Set(ref selectedOrderService,value); }


        #region PlusMinusAmount - Command
        private RelayCommand? plusAmountCmd;
        public RelayCommand? PlusAmountCmd => plusAmountCmd ??= new(obj => AddAmount(), obj => SelectedOrderService != null);
        private void AddAmount()
        {
            SelectedOrderService.Amount++;
        }
        private RelayCommand? minusAmountCmd;
        public RelayCommand? MinusAmountCmd => minusAmountCmd ??= new(obj => MinusAmount(), obj => SelectedOrderService != null && SelectedOrderService.Amount > 1);
        private void MinusAmount()
        {
            SelectedOrderService.Amount--;
        } 
        #endregion

        private RelayCommand? addService;
        public RelayCommand? AddServiceCmd => addService ??= new(obj => AddService());
        private void AddService()
        {
            userDialog.OpenServices();
        }

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
