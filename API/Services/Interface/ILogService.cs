using webshopAPI.Models;

namespace webshopAPI.Services.Interface
{
    public interface ILogService
    {
        IEnumerable<Log> GetLatestLogs(int count);
        int GetLogCount();
        void LogAction(string level, string message);
    }
}
