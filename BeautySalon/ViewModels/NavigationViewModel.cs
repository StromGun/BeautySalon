using BeautySalon.Commands;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;

namespace BeautySalon.ViewModels
{
    internal class NavigationViewModel : ViewModel
    {
        private readonly ICountsService countsService;
        private readonly IOrderService orderService;


        private string _title = "Navigation";
        public string Title { get => _title; set => Set(ref _title,value); }

        private int newClients;
        public int NewClientsCount { get => newClients; set => Set(ref newClients, value); }

        private int todayOrderCount;
        public int TodayOrderCount { get => todayOrderCount; set => Set(ref todayOrderCount, value); }

        private int newWeeklyClients;
        public int NewWeeklyClientsCount { get => newWeeklyClients; set => Set(ref newWeeklyClients, value); }

        private int weeklyOrderCount;
        public int WeeklyOrderCount { get => weeklyOrderCount; set => Set(ref weeklyOrderCount, value); }

        private int newMonthlyClientsCount;
        public int NewMonthlyClientsCount { get => newMonthlyClientsCount; set => Set(ref newMonthlyClientsCount, value); }

        private int monthlyOrdersCount;
        public int MonthlyOrdersCount { get => monthlyOrdersCount; set => Set(ref monthlyOrdersCount, value); }

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

                    orderViewSource?.View.Refresh();
                    OnPropertyChanged(nameof(OrdersView));
                }
            }
        }
        #endregion

        
        private RelayCommand? loaded;
        public RelayCommand? Loaded => loaded ??= new(async obj => await Load());


        private async Task Load()
        {     
            NewClientsCount = await countsService.GetCountNewClient();
            TodayOrderCount = await countsService.GetCountTodayOrders();
            NewWeeklyClientsCount = await countsService.GetWeeklyNewClient();
            WeeklyOrderCount = await countsService.GetWeeklyOrders();
            NewMonthlyClientsCount = await countsService.GetMonthlyNewClient();
            MonthlyOrdersCount = await countsService.GetMonthlyOrders();

            var orde = orderService.Orders?.Where(o =>
                o.DateStart.Value.Date == DateTime.Now.Date &&
                o.TimeStart >= DateTime.Now.TimeOfDay &&
                o.Status == StatusOrder.Выполняется).OrderBy(o => o.TimeStart).Take(6).ToList();
            Orders = orde is null ? new() : new(orde);
        }

        public NavigationViewModel(ICountsService countsService, IOrderService orderService)
        {
            this.countsService = countsService;
            this.orderService = orderService;

        }
    }
}
