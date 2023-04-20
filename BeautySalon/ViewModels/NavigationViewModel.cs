using BeautySalon.Commands;
using BeautySalon.ViewModels.Base;

namespace BeautySalon.ViewModels
{
    internal class NavigationViewModel : ViewModel
    {
        private string _title = "Navigation";
        public string Title { get => _title; set => Set(ref _title,value); }
        

    }
}
