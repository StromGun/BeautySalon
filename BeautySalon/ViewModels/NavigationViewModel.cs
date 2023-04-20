using BeautySalon.Commands;
using BeautySalon.ViewModels.Base;

namespace BeautySalon.ViewModels
{
    internal class NavigationViewModel : ViewModel
    {
        private string _title = "Navigation";
        public string Title { get => _title; set => Set(ref _title,value); }


        public delegate void NavigationEventHandler();
        public event NavigationEventHandler? OpenClientViewExeccuted;
        public event NavigationEventHandler? OpenOrderViewExecuted;

        private RelayCommand? openClientViewCmd;
        public RelayCommand? OpenClientViewCmd => openClientViewCmd ??= new(obj => OpenClientView());
        private void OpenClientView()
        {
            OpenClientViewExeccuted?.Invoke();
        }

        private RelayCommand? openOrderViewCmd;
        public RelayCommand? OpenOrderViewCmd => openOrderViewCmd ??= new(obj => OpenOrderView());
        private void OpenOrderView()
        {
            OpenOrderViewExecuted?.Invoke();
        }

    }
}
