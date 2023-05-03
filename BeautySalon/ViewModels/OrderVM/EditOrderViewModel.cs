using BeautySalon.Commands;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace BeautySalon.ViewModels
{
    internal class EditOrderViewModel : ViewModel
    {
        private readonly IUserDialog userDialog = App.Services.GetRequiredService<IUserDialog>();


        private ObservableCollection<OrderService>? orderServiceCollection;
        public ObservableCollection<OrderService>? OrderService { get => orderServiceCollection; set => Set(ref orderServiceCollection,value); }

        private ObservableCollection<Service>? serviceCollection;
        public ObservableCollection<Service>? ServiceCollection { get => serviceCollection; set => Set(ref serviceCollection, value); }

        private BindingList<OrderService>? orderServiceBindingList;
        public BindingList<OrderService>? OrderServiceBindingList { get => orderServiceBindingList; set => Set(ref orderServiceBindingList, value); }

        private Order? order;
        public Order? Order { get => order; set => Set(ref order,value); }

        private Client? client = new();



        private OrderService? selectedOrderService;
        public OrderService? SelectedOrderService { get => selectedOrderService; set { Set(ref selectedOrderService, value); } }


        #region PlusMinusAmount - Command
        private RelayCommand? plusAmountCmd;
        public RelayCommand? PlusAmountCmd => plusAmountCmd ??= new(obj => AddAmount(), obj => SelectedOrderService != null);
        private void AddAmount()
        {
            SelectedOrderService!.Amount++;
        }
        private RelayCommand? minusAmountCmd;
        public RelayCommand? MinusAmountCmd => minusAmountCmd ??= new(obj => MinusAmount(), obj => SelectedOrderService != null && SelectedOrderService.Amount > 1);
        private void MinusAmount()
        {
            SelectedOrderService!.Amount--;
        }
        #endregion

        #region AddService - Command
        private RelayCommand? addService;
        public RelayCommand? AddServiceCmd => addService ??= new(obj => AddService());
        private void AddService()
        {
            if (userDialog.OpenServices(ServiceCollection!) != true) return;

            try
            {
                foreach (var item in ServiceCollection!)
                {
                    bool equal = false;
                    foreach (var oitem in OrderServiceBindingList!)
                    {
                        if (oitem.Service!.ID == item.ID)
                        {
                            oitem.Amount++;
                            equal = true;
                            break;
                        }
                        else equal = false;
                    }
                    if (!equal)
                        OrderServiceBindingList!.Add(new OrderService() { Service = item, Amount = 1, Order = Order });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ServiceCollection!.Clear();
        }
        #endregion

        #region RemoveService - Command
        private RelayCommand? removeService;
        public RelayCommand? RemoveServiceCmd => removeService ??= new(obj => RemoveService(), obj => SelectedOrderService != null);
        private void RemoveService()
        {
            OrderServiceBindingList!.Remove(SelectedOrderService!);
        }

        
        #endregion

        #region SelectClient - Command
        private RelayCommand? selectClient;
        public RelayCommand? SelectClientCmd => selectClient ??= new(obj => SelectClient(), obj => Order!.Client == null);
        private void SelectClient()
        {

            if (userDialog.OpenClientList(ref client!) != true) return;

            Order!.Client = client;

        } 
        #endregion

        private void OrderServiceBindingList_ListChanged(object? sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                foreach (var item in OrderServiceBindingList!)
                    item.TotalPrice = item.Service!.Price * item.Amount - item.Service.Price * item.Amount * item.Discount;

                var sum = new TimeSpan(OrderServiceBindingList.Sum(x => x.Service!.Time!.Value.Ticks));
                Order!.TimeEnd = Order.TimeStart + sum;
                Order!.TotalPrice = OrderServiceBindingList.Sum(x => x.TotalPrice);

                MessageBox.Show(e.ListChangedType.ToString());
            }
        }



        public EditOrderViewModel(Order? order)
        {
            ServiceCollection = new();

            Order = new()
            {
                ID = order!.ID,
                Client = order.Client,
                DateStart = order.DateStart,
                OrderName = order.OrderName,
                OrderServices = order.OrderServices,
                Services = order.Services,
                Status = order.Status,
                TimeStart = order.TimeStart,
                TimeEnd = order.TimeEnd,
                TotalPrice = order.TotalPrice
            };

            if (order.OrderServices != null)
            {
                OrderServiceBindingList = new(order.OrderServices.ToList());
            }
            else OrderServiceBindingList = new();

            OrderServiceBindingList!.ListChanged += OrderServiceBindingList_ListChanged;
        }
    }
}
