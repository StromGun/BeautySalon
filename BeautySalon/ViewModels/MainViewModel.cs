using BeautySalon.ViewModels.Base;

namespace BeautySalon.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        private readonly NavigationViewModel navigationViewModel;
        private readonly ClientViewModel clientView;
        private readonly OrdersViewModels ordersView;
        private ViewModel? currentViewModel;
        public ViewModel? CurrentViewModel { get => currentViewModel; set => Set(ref currentViewModel, value); }

        private string _title = "Кунгурская типография";
        public string Title { get => _title; set => Set(ref _title, value); }

        public MainViewModel(
            NavigationViewModel navigationViewModel,
            ClientViewModel clientView,
            OrdersViewModels ordersView)
        {
            this.navigationViewModel = navigationViewModel;
            this.clientView = clientView;
            this.ordersView = ordersView;
            CurrentViewModel = this.navigationViewModel;

            navigationViewModel.OpenClientViewExeccuted += NavigationViewModel_OpenClientViewExeccuted;
            navigationViewModel.OpenOrderViewExecuted += NavigationViewModel_OpenOrderViewExecuted;
        }

        private void NavigationViewModel_OpenOrderViewExecuted()
        {
            CurrentViewModel = ordersView;
        }

        private void NavigationViewModel_OpenClientViewExeccuted()
        {
            CurrentViewModel = clientView;
        }
    }
}
