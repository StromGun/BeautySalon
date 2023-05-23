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

        private DateTime datetime;
        public DateTime DateTime { get => datetime; set => Set(ref datetime, value); }

        private int newClients;
        public int NewClientsCount { get => newClients; set => Set(ref newClients, value); }

        private int todayOrderCount;
        public int TodayOrderCount { get => todayOrderCount; set => Set(ref todayOrderCount, value); }

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

        #region Timer
        private void Timer()
        {
            DispatcherTimer timer = new(DispatcherPriority.Render)
            {
                Interval = TimeSpan.FromSeconds(1),
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            DateTime = DateTime.Now;
        } 
        #endregion

        private RelayCommand? loaded;
        public RelayCommand? Loaded => loaded ??= new(async obj => await Load());
        private async Task Load()
        {     
            NewClientsCount = await countsService.GetCountNewClient();
            TodayOrderCount = await countsService.GetCountTodayOrders();

            var orde = orderService.Orders?.Where(o =>
                o.DateStart.Value.Date == DateTime.Now.Date &&
                o.TimeStart >= DateTime.TimeOfDay &&
                o.Status == StatusOrder.Выполняется).OrderBy(o => o.TimeStart).Take(3).ToList();
            Orders = orde is null ? new() : new(orde);
        }

        public NavigationViewModel(ICountsService countsService, IOrderService orderService)
        {
            this.countsService = countsService;
            this.orderService = orderService;
            Timer();
        }
    }
}
