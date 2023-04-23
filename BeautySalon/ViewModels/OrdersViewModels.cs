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

        public List<ScheduleItem>? ScheduleItems { get; set; }

        private ObservableCollection<Day>? days;
        public ObservableCollection<Day>? Days { get => days; set => Set(ref days,value); }

        private ObservableCollection<Order>? orders;
        public ObservableCollection<Order>? Orders { get => orders; set => Set(ref orders, value); }

        public OrdersViewModels(IOrderService orderService)
        {
            Days = new()
            {
                new Day { WeekNo = 1, WeekDay = 2, Date = DateTime.Now },
                new Day {WeekNo = 3, WeekDay = 3, Date = DateTime.Now },
                new Day { WeekNo = 2, WeekDay = 2, Date = DateTime.Now},
                new Day { WeekNo = 2, WeekDay = 7, Date = DateTime.Now},
                new Day { WeekNo = 3, WeekDay = 7, Date = DateTime.Now},
                new Day { WeekNo = 4, WeekDay = 7, Date = DateTime.Now},
                new Day { WeekNo = 5, WeekDay = 7, Date = DateTime.Now},
                new Day { WeekNo = 6, WeekDay = 7, Date = DateTime.Now},
            };
            this.orderService = orderService;

            ScheduleItems = new();
            
            Orders = this.orderService.Orders;
            ScheduleItems.Add(new() { Title = Orders?.FirstOrDefault()?.OrderName });
        }
    }

    public class Day
    {
        public int WeekNo { get; set; }
        public int WeekDay { get; set; }
        public DateTime Date { get; set; }
    }
}
