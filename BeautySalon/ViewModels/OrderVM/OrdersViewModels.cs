using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace BeautySalon.ViewModels
{
    class OrdersViewModels : ViewModel
    {
        private readonly IOrderService orderService;



        private string _title = "Записи";
        public string Title { get => _title; set => Set(ref _title,value); }

        private DateTime selectedDate;
        public DateTime SelectedDate { get => selectedDate; set { Set(ref selectedDate, value); orderViewSource?.View.Refresh(); } }

        private ObservableCollection<OrderService>? orderServices;
        public ObservableCollection<OrderService>? OrderServices { get => orderServices; set => Set(ref orderServices,value); }

        private Order? selectedOrder;
        public Order? SelectedOrder { get => selectedOrder; set => Set(ref selectedOrder, value); }

        #region Statuses
        private bool statusIsPerformed;
        public bool StatusIsPerformed { get => statusIsPerformed; set { Set(ref statusIsPerformed, value); orderViewSource?.View.Refresh(); } }
        private bool statusCanceled;
        public bool StatusCanceled { get => statusCanceled; set { Set(ref statusCanceled, value); orderViewSource?.View.Refresh(); } }
        private bool statusPerformed;
        public bool StatusPerformed { get => statusPerformed; set { Set(ref statusPerformed, value); orderViewSource?.View.Refresh(); } } 
        #endregion

        #region Orders
        private ObservableCollection<Order>? orders;
        private CollectionViewSource? orderViewSource;
        public ICollectionView? OrdersView => orderViewSource?.View;
        public ObservableCollection<Order>? Orders
        {
            get => orders;
            set
            {
                if (Set(ref orders, value))
                {
                    orderViewSource = new()
                    {
                        Source = value,
                        SortDescriptions = { new SortDescription(nameof(Order.TimeStart), ListSortDirection.Ascending) }
                    };

                    orderViewSource.Filter += SelectedDateFilter;

                    orderViewSource?.View.Refresh();
                    OnPropertyChanged(nameof(OrdersView));
                }
            }
        }

        private void SelectedDateFilter(object sender, FilterEventArgs e)
        {
            if (e.Item is Order order)
            {
                if (order?.DateStart != null)
                    if (order?.DateStart.Value.Date == SelectedDate.Date)
                    {
                        if (StatusIsPerformed)
                            if (order.Status != StatusOrder.Выполняется)
                                e.Accepted = false;
                        if (StatusCanceled)
                            if (order.Status != StatusOrder.Отменен)
                                 e.Accepted = false;
                        if (StatusPerformed)
                            if (order.Status != StatusOrder.Выполнен)
                                e.Accepted = false;
                        if (StatusIsPerformed && StatusCanceled && StatusPerformed)
                            e.Accepted = true;
                    }
                    else e.Accepted = false;
                else e.Accepted = false;
            }
        }
        #endregion

        public OrdersViewModels(IOrderService orderService)
        {
            this.orderService = orderService;
            SelectedDate = DateTime.Now;

            Orders = this.orderService.Orders;
            OrderServices = this.orderService.OrderServices;

        }
    }
}
