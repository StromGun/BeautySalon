﻿using BeautySalon.DAL.Entities;
using BeautySalon.Resources.UserControls;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
                        SortDescriptions = { new SortDescription(nameof(Order.DateStart), ListSortDirection.Ascending) }
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
                if (order?.DateStart.Value.Date == SelectedDate.Date)
                    e.Accepted = true;
                else e.Accepted = false;
            }
        }
        #endregion

        public OrdersViewModels(IOrderService orderService)
        {
            this.orderService = orderService;
            SelectedDate = DateTime.Now;

            Orders = this.orderService.Orders;

        }
    }
}
