using BeautySalon.Commands;
using BeautySalon.DAL.Entities;
using BeautySalon.Services.Interfaces;
using BeautySalon.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace BeautySalon.ViewModels
{
    internal class ServicesViewModel : ViewModel
    {
        private readonly IServicesService servicesService = App.Services.GetRequiredService<IServicesService>();

        private string title = "Услуги";
        public string Title { get => title; set => Set(ref title,value); }

        /// <summary>
        /// Список услуг
        /// </summary>
        private ObservableCollection<Service>? _services;
        public ObservableCollection<Service>? Services { get => _services; set => Set(ref _services, value); }

        #region SelectedService
        /// <summary>
        /// Выбранная услуга из левого столбца
        /// </summary>
        private Service? selectedServiceL;
        public Service? SelectedServiceL { get => selectedServiceL; set => Set(ref selectedServiceL, value); }

        /// <summary>
        /// Выбранная услуга из правого столбца
        /// </summary>
        private Service? selectedServiceR;
        public Service? SelectedServiceR { get => selectedServiceR; set => Set(ref selectedServiceR, value); } 
        #endregion

        /// <summary>
        /// Список категорий услуг
        /// </summary>
        private ObservableCollection<ServiceType>? _serviceTypes;
        public ObservableCollection<ServiceType>? ServiceTypes { get => _serviceTypes; set => Set(ref _serviceTypes, value); }

        /// <summary>
        /// Список услуг из правого столбца
        /// </summary>
        private ObservableCollection<Service>? selectedServices;
        public ObservableCollection<Service>? SelectedServices { get => selectedServices; set => Set(ref selectedServices, value); }

        #region AddServiceCmd - Command
        private RelayCommand? addServiceCmd;
        public RelayCommand? AddServiceCmd => addServiceCmd ??= new(obj => AddService(), obj => CanAddService());
        private bool CanAddService() => SelectedServiceL != null;
        private void AddService()
        {
            try
            {
                // ненужная проверка.
                if (SelectedServices!.Count > 0)
                {
                    // эквивалентно
                    bool equal = false;
                    foreach ( var item in SelectedServices!.ToList())
                    {
                        // проверка на присутствие услуги в правом столбце
                        if (item.ID == SelectedServiceL!.ID)
                        {
                            equal = true;
                            break;
                        }
                    }
                    // если услуги нет в правом столбце добавляем в список выбранную услугу
                    if (!equal) 
                        SelectedServices!.Add(SelectedServiceL!);
                }
                else SelectedServices!.Add(SelectedServiceL!);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region DeleteServiceCmd - Command
        private RelayCommand? deleteServiceCmd;
        public RelayCommand? DeleteServiceCmd => deleteServiceCmd ??= new(obj => DeleteService(), obj => CanDeleteService());
        private bool CanDeleteService() => SelectedServiceR != null;
        private void DeleteService()
        {
            SelectedServices!.Remove(SelectedServiceR!);
        } 
        #endregion

        public ServicesViewModel(ICollection services )
        {
            Services = servicesService.Services;
            ServiceTypes = servicesService.ServiceTypes;
            SelectedServices = (ObservableCollection<Service>?)services;
        }

    }
}
