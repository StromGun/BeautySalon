using BeautySalon.Commands;
using BeautySalon.ViewModels.Base;
using System;

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

        #region OpenClientViewModel - Command
        private RelayCommand? openClientView;
        public RelayCommand? OpenClientViewCmd => openClientView ??= new(obj => OpenClientViewExecuted(), obj => CanOpenClientView());
        private bool CanOpenClientView() => CurrentViewModel != clientView;
        private void OpenClientViewExecuted() => ChangeViewModel(clientView);
        #endregion

        #region OpenOrderView - Command
        private RelayCommand? openOrderViewCmd;
        public RelayCommand? OpenOrderViewCmd => openOrderViewCmd ??= new(obj => OpenOrderView(), obj => CanOpenOrderView());
        private bool CanOpenOrderView() => CurrentViewModel != ordersView;
        private void OpenOrderView() => ChangeViewModel(ordersView); 
        #endregion

        private void ChangeViewModel(ViewModel viewModel)
        {
            CurrentViewModel = viewModel;
        }

        public MainViewModel(
            NavigationViewModel navigationViewModel,
            ClientViewModel clientView,
            OrdersViewModels ordersView)
        {
            this.navigationViewModel = navigationViewModel;
            this.clientView = clientView;
            this.ordersView = ordersView;
            CurrentViewModel = this.navigationViewModel;
        }

    }
}
