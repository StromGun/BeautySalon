using BeautySalon.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon.ViewModels
{
    internal class ServicesViewModel : ViewModel
    {
        private string title = "Услуги";
        public string Title { get => title; set => Set(ref title,value); }

    }
}
