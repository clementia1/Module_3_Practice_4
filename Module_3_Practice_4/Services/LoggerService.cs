using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_3_Practice_4.Services
{
    public class LoggerService
    {
        public event Action CreateBackup;

        public void Log(LogType type, string message)
        {
            var consoleMessage = $"{DateTime.UtcNow}: {type}: {message}";
            _fileService.WriteLine(consoleMessage);
        }
    }
}
