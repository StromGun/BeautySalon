using BeautySalon.Commands;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using System;
using System.Windows;

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

        private User? user;
        public User? User { get => user; set => Set(ref user, value); }

        private string _title = "CRM Beauty Salon";
        public string Title { get => _title; set => Set(ref _title, value); }

        #region OpenNavigation - Command
        private RelayCommand? openNavigation;
        public RelayCommand? OpenNavigationCmd => openNavigation
            ??= new(obj => ChangeViewModel(navigationViewModel), obj => CurrentViewModel != navigationViewModel); 
        #endregion

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

        #region Logout - Command
        private RelayCommand? logoutCmd;
        public RelayCommand? LogoutCmd => logoutCmd ??= new(obj => LogoutExecuted(), obj => CurrentViewModel != authorizationViewModel);
        private void LogoutExecuted()
        {
            CurrentViewModel = authorizationViewModel;
            User = null;
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
            if (sender is User)
            {
                User = (User)sender;
                //CurrentViewModel = clientView;
                CurrentViewModel = navigationViewModel;
            }
            else MessageBox.Show($"Sender in not User. MainViewModel");
        }
    }
}
