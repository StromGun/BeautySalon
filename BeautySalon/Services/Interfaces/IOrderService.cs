﻿using BeautySalon.DAL.Entities;
using System.Collections.ObjectModel;

namespace BeautySalon.Services.Interfaces
{
    interface IOrderService
    {
        public ObservableCollection<Order>? Orders { get; set; }
        public ObservableCollection<OrderService>? OrderServices { get; set; }


        protected ObservableCollection<Order>? GetOrders();
        protected ObservableCollection<OrderService>? GetOrderServices();

    }
}
