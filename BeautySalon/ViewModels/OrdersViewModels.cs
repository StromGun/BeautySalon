using BeautySalon.DAL.Entities;
using BeautySalon.Resources.UserControls;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BeautySalon.ViewModels
{
    class OrdersViewModels : ViewModel
    {
        private readonly IOrderService orderService;



        private string _title = "Записи";
        public string Title { get => _title; set => Set(ref _title,value); }

        private ObservableCollection<Order>? orders;
        public ObservableCollection<Order>? Orders { get => orders; set => Set(ref orders, value); }

        public OrdersViewModels(IOrderService orderService)
        {
            this.orderService = orderService;
            
            Orders = this.orderService.Orders;

        }
    }
}
