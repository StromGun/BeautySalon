using System.Threading.Tasks;

namespace BeautySalon.Services.Interfaces
{
    interface ICountsService
    {
        Task<int> GetCountNewClient();
        Task<int> GetCountTodayOrders();
    }
}
