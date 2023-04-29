using BeautySalon.DAL.Entities;
using BeautySalon.ViewModels.Base;

namespace BeautySalon.ViewModels
{
    internal class EditOrderViewModel : ViewModel
    {
        private Order? order;
        public Order? Order { get => order; set => Set(ref order,value); }

        public EditOrderViewModel(Order? order)
        {
            Order = order;
        }
    }
}
