using BeautySalon.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon.ViewModels
{
    class OrdersViewModels : ViewModel
    {
        private string _title = "Записи";
        public string Title { get => _title; set => Set(ref _title,value); }

        private ObservableCollection<Day>? days;
        public ObservableCollection<Day>? Days { get => days; set => Set(ref days,value); }

        public OrdersViewModels()
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
        }
    }

    public class Day
    {
        public int WeekNo { get; set; }
        public int WeekDay { get; set; }
        public DateTime Date { get; set; }
    }
}
