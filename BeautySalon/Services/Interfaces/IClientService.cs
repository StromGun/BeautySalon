using BeautySalon.DAL.Entities;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BeautySalon.Services.Interfaces
{
    public interface IClientService
    {
        public ObservableCollection<Client>? Clients { get; protected set; }

        protected ObservableCollection<Client>? GetClients();
    }
}
