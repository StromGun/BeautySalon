using System.Threading.Tasks;

namespace BeautySalon.Services.Interfaces
{
    interface ICountsService
    {
        Task<int> GetCountNewClient();
        Task<int> GetCountTodayOrders();

        Task<int> GetWeeklyNewClient();
        Task<int> GetWeeklyOrders();

        Task<int> GetMonthlyNewClient();
        Task<int> GetMonthlyOrders();
    }
}
