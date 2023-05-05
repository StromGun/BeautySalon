using BeautySalon.DAL.Entities;
using BeautySalon.ViewModels.Base;

namespace BeautySalon.ViewModels
{
    internal class EditServiceCategoryViewModel : ViewModel
    {
        private ServiceType? serviceType;
        public ServiceType? ServiceType { get => serviceType; set => Set(ref serviceType, value); }

        public EditServiceCategoryViewModel(ServiceType serviceType)
        {
            ServiceType = new()
            {
                Name = serviceType.Name,
            };
        }
    }
}
