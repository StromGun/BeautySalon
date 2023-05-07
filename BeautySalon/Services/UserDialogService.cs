using System.Collections;
using System.Windows;
using System.Windows.Controls;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels;
using BeautySalon.Views.Windows;
using Microsoft.Win32;

namespace BeautySalon.Services
{
    internal class UserDialogService : IUserDialog
    {
      
        public bool EditClient(ref Client? client)
        {
            if (client == null) return false;
            var client_editor_model = new EditClientViewModel(client);
            var client_editor_window = new EditClientWindow()
            {
                DataContext = client_editor_model,
                Owner = App.ActiveWindow
            };

            if (client_editor_window.ShowDialog() != true) 
                return false;

            client.LastName = client_editor_model.Client.LastName;
            client.FirstName = client_editor_model.Client.FirstName;
            client.Patronymic = client_editor_model.Client.Patronymic;
            client.Status = client_editor_model.Client.Status;
            client.Email = client_editor_model.Client.Email;
            client.Gender = client_editor_model.Client.Gender;
            client.Phone = client_editor_model.Client.Phone;
            client.BirthDay = client_editor_model.Client.BirthDay;
            client.Image = client_editor_model.Client.Image;

            return true;
        }

        public bool EditOrder (ref Order? order)
        {
            if (order == null) return false;
            var order_editor_model = new EditOrderViewModel(order);
            var order_editor_window = new EditOrderWindow()
            {
                DataContext = order_editor_model,
                Owner = App.ActiveWindow
            };

            if (order_editor_window.ShowDialog() != true)
                return false;

            order.Client = order_editor_model.Order!.Client;
            order.OrderName = order_editor_model.Order.OrderName;
            order.Status = order_editor_model.Order.Status;
            order.DateStart = order_editor_model.Order.DateStart;
            order.TimeStart = order_editor_model.Order.TimeStart;
            order.TimeEnd = order_editor_model.Order.TimeEnd;
            order.OrderServices = order_editor_model.OrderServiceBindingList;
            order.TotalPrice = order_editor_model.Order.TotalPrice;

            return true;
        }

        public bool EditServiceCategory(ref ServiceType serviceType)
        {
            if (serviceType == null) return false;
            var serviceType_editor_model = new EditServiceCategoryViewModel(serviceType);
            var serviceType_editor_window = new EditServiceCategoryWindow()
            {
                DataContext = serviceType_editor_model,
                Owner = App.ActiveWindow
            };

            if (serviceType_editor_window.ShowDialog() != true)
                return false;

            serviceType.Name = serviceType_editor_model.ServiceType?.Name;
            

            return true;
        }

        public bool EditService(ref Service service)
        {
            if (service == null) return false;
            var service_editor_model = new EditServiceViewModel(service);
            var service_editor_window = new EditServiceWindow()
            {
                DataContext = service_editor_model,
                Owner = App.ActiveWindow
            };

            if (service_editor_window.ShowDialog() != true)
                return false;

            service.Name = service_editor_model.Service!.Name;
            service.Price = service_editor_model.Service!.Price;
            service.ServiceType = service_editor_model.Service.ServiceType;
            service.Time = service_editor_model.Service.Time;


            return true;
        }


        public bool OpenServices(ICollection services)
        {
            var services_model = new ServicesViewModel(services);
            var services_window = new ServicesWindow()
            {
                DataContext = services_model,
                Owner = App.ActiveWindow
            };

            if (services_window.ShowDialog() != true)
                return false;

            return true;
        }

        public bool OpenClientList(ref Client client)
        {
            var client_model = new ClientListViewModel();
            var client_window = new ClientListWindow()
            {
                DataContext = client_model,
                Owner = App.ActiveWindow
            };

            if (client_window.ShowDialog() != true)
                return false;

            client = client_model.Client!;

            return true;
        }

        public bool OpenAboutBox()
        {
            var aboutBox = new AboutWindow()
            {
                Owner = App.ActiveWindow
            };
            aboutBox.ShowDialog();
            return true;
        }

        public bool OpenFileDialog(ref string path)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Image Files(*.png, *.jpg, *.jpeg, *.bmp, *.gif)|*.png; *.jpg; *.jpeg; *.bmp"
            };
            if (openFileDialog.ShowDialog() != true) return false;

            path = openFileDialog.FileName;
            
            return true;
        }

        public bool ConfirmedInformation(string Information, string Caption) => MessageBox
            .Show(
            Information, Caption,
            MessageBoxButton.YesNo,
            MessageBoxImage.Information)
            == MessageBoxResult.Yes;

        public bool ConfirmError(string Error, string Caption) => MessageBox
             .Show(
            Error, Caption,
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning)
            == MessageBoxResult.Yes;

        public bool ConfirmedWarning(string Warning, string Caption) => MessageBox
             .Show(
            Warning, Caption,
            MessageBoxButton.YesNo,
            MessageBoxImage.Error)
            == MessageBoxResult.Yes;
    }
}
