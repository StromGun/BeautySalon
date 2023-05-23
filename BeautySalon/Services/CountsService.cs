using BeautySalon.DAL.Context;
using BeautySalon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon.Services
{
    class CountsService : ICountsService, INotifyPropertyChanged
    {
        private readonly BeautySalonDB db;

        public CountsService(BeautySalonDB db)
        {
            this.db = db;
        }

        public async Task<int> GetCountNewClient()
        {
           return await db.Clients.Where(c => c.DateAdded.Date == DateTime.Now.Date).CountAsync();
        }


        public async Task<int> GetCountTodayOrders()
        {
            return await db.Orders.Where(c => c.DateStart == DateTime.Now.Date).CountAsync();
        }


        #region INPC

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public bool Set<T>(ref T field, T value, [CallerMemberName] string? prop = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(prop);
            return true;
        }
        #endregion
    }
}
