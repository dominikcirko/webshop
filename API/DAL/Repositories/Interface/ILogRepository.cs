using webshopAPI.Models;

namespace webshopAPI.DAL.Repositories.Interface
{
    public interface ILogRepository
    {
        IEnumerable<Log> GetLatestLogs(int count);
        int GetLogCount();
        void Add(Log log);
    }
}
