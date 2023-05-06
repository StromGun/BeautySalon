using BeautySalon.Commands;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;

namespace BeautySalon.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        private readonly AuthorizationViewModel authorizationViewModel;
        private readonly NavigationViewModel navigationViewModel;
        private readonly ClientViewModel clientView;
        private readonly OrdersViewModels ordersView;
        private readonly ServicesListViewModel servicesListViewModel;
        private readonly IUserDialog userDialog;

        private ViewModel? currentViewModel;
        public ViewModel? CurrentViewModel { get => currentViewModel; set => Set(ref currentViewModel, value); }

        private string _title = "CRM Beauty Salon";
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

        #region OpenServiceView - Command
        private RelayCommand? openServiceViewCmd;
        public RelayCommand? OpenServiceViewCmd => openServiceViewCmd ??= new(obj => OpenServiceView(), obj => CanOpenServiceView());
        private bool CanOpenServiceView() => CurrentViewModel != servicesListViewModel;
        private void OpenServiceView() => ChangeViewModel(servicesListViewModel);
        #endregion

        #region OpenAboutBox - Command
        private RelayCommand? openAboutBox;
        public RelayCommand? OpenAboutBoxCmd => openAboutBox ??= new(obj => OpenAboutBox());
        private void OpenAboutBox()
        {
            userDialog.OpenAboutBox();
        }
        #endregion

        private void ChangeViewModel(ViewModel viewModel)
        {
            CurrentViewModel = viewModel;
        }

        public MainViewModel(
            AuthorizationViewModel authorizationViewModel,
            NavigationViewModel navigationViewModel,
            ClientViewModel clientView,
            OrdersViewModels ordersView,
            ServicesListViewModel servicesListViewModel,
            IUserDialog userDialog)
        {
            this.authorizationViewModel = authorizationViewModel;
            this.navigationViewModel = navigationViewModel;
            this.clientView = clientView;
            this.ordersView = ordersView;
            this.servicesListViewModel = servicesListViewModel;
            this.userDialog = userDialog;


            CurrentViewModel = this.authorizationViewModel;


            authorizationViewModel.IsAuthorizated += AuthorizationViewModel_IsAuthorizated;
        }

        private void AuthorizationViewModel_IsAuthorizated(object? sender, AuthorizationViewModel e)
        {
            CurrentViewModel = navigationViewModel;
        }
    }
}
