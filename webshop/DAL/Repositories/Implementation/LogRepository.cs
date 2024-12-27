using webshopAPI.DAL.Repositories.Interface;
using webshopAPI.Models;

namespace webshopAPI.DAL.Repositories.Implementation
{
    public class LogRepository : ILogRepository
    {
        private readonly webshopdbContext _context;

        public LogRepository(webshopdbContext context)
        {
            _context = context;
        }

        public IEnumerable<Log> GetLatestLogs(int count)
        {
            return _context.Logs
                .OrderByDescending(l => l.Timestamp)
                .Take(count)
                .ToList();
        }

        public int GetLogCount()
        {
            return _context.Logs.Count();
        }

        public void Add(Log log)
        {
            _context.Logs.Add(log);
            _context.SaveChanges();
        }
    }
}
